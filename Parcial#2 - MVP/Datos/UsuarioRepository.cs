using MySql.Data.MySqlClient;
using Parcial_2___MVP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_2___MVP.Datos
{
    internal class UsuarioRepository
    {
        // Esta es la ruta hacia tu "archivero" de MySQL. 
        // Cambia la contraseña por la de tu usuario root.
        private string _cadenaConexion = "Server=localhost;Database=LaboratorioMVP;Uid=root;Pwd=1234;";

        // Método que busca en MySQL si existe un correo específico
        public Usuario ObtenerUsuarioPorCorreo(string correo)
        {
            Usuario usuarioEncontrado = null;

            using (MySqlConnection conexion = new MySqlConnection(_cadenaConexion))
            {
                string query = "SELECT Id, Nombre, Correo, PasswordHash, Salt FROM Usuarios WHERE Correo = @correo";

                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@correo", correo); // Evita inyecciones SQL

                conexion.Open();
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Si encuentra el correo, empaca los datos en la Entidad y los devuelve
                        usuarioEncontrado = new Usuario
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = reader["Nombre"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Salt = reader["Salt"].ToString()
                        };
                    }
                }
            }

            return usuarioEncontrado; // Devuelve los datos, o devuelve null si no existe
        }

        public void GuardarUsuario(string nombre, string correo, string passwordHash, string salt)
        {
            using (MySqlConnection conexion = new MySqlConnection(_cadenaConexion))
            {
                string query = "INSERT INTO Usuarios (Nombre, Correo, PasswordHash, Salt) VALUES (@nombre, @correo, @hash, @salt)";
                MySqlCommand comando = new MySqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@correo", correo);
                comando.Parameters.AddWithValue("@hash", passwordHash);
                comando.Parameters.AddWithValue("@salt", salt);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
    }
}
