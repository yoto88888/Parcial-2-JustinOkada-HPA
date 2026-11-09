using Parcial_2___MVP.Datos;
using Parcial_2___MVP.Entidades;
using Parcial_2___MVP.Negocio;
using Parcial_2___MVP.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial_2___MVP.Presentadores
{
    public class LoginPresenter
    {
        private Form1 _vista;
        private UsuarioRepository _repositorio;
        private PasswordSeguridadService _seguridadService;

        public LoginPresenter(Form1 vista)
        {
            _vista = vista;
            _repositorio = new UsuarioRepository();
            _seguridadService = new PasswordSeguridadService();
        }

        public void IntentarLogin(string correo, string contrasena)
        {
            // 1. Le pedimos a la capa de datos que busque el correo
            Usuario usuario = _repositorio.ObtenerUsuarioPorCorreo(correo);

            if (usuario != null)
            {
                // 2. Si el usuario existe, verificamos la contraseña usando tu contrato
                bool esValida = _seguridadService.Verificar(contrasena, usuario.PasswordHash, usuario.Salt);

                if (esValida)
                {
                    // 3. Si todo coincide, le damos la orden a la vista
                    _vista.MostrarUsuarioLogeado(usuario.Nombre);
                    MessageBox.Show("¡Bienvenido " + usuario.Nombre + "!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Contraseña incorrecta.", "Error de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("El correo no está registrado.", "Usuario no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void RegistrarUsuario(string correo, string contrasena)
        {
            // Usamos tu contrato IHashService para generar el Hash y Salt verdaderos
            var resultado = _seguridadService.GenerarHash(contrasena);

            // Guardamos en MySQL usando la Capa de Datos
            _repositorio.GuardarUsuario("Usuario!", correo, resultado.Hash, resultado.Salt);

            MessageBox.Show("¡Usuario registrado en la base de datos con éxito!", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
