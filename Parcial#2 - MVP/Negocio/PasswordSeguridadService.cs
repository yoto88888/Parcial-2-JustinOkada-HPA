using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_2___MVP.Negocio
{
    public class PasswordSeguridadService : IHashService, IVerificarHashService
    {
        public (string Hash, string Salt) GenerarHash(string passwordTextoPlano)
        {
            // Generar un Salt aleatorio
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);

            // Generar el Hash combinando el password y el salt
            string hash = CrearHashSha256(passwordTextoPlano + salt);
            return (hash, salt);
        }

        public bool Verificar(string passwordTextoPlano, string hashAlmacenado, string saltAlmacenado)
        {
            string hashIntento = CrearHashSha256(passwordTextoPlano + saltAlmacenado);
            return hashIntento == hashAlmacenado;
        }

        private string CrearHashSha256(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
