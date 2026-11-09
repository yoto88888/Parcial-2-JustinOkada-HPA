using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Parcial_2___MVP.Presentadores;

namespace Parcial_2___MVP.Vistas
{
    public partial class Form1 : Form
    {
        private LoginPresenter _presentador;
        public Form1()
        {
            InitializeComponent();
            _presentador = new LoginPresenter(this);
        }

        // Método que cumple el requisito de mostrar la sesión en el panel lateral
        public void MostrarUsuarioLogeado(string nombreUsuario)
        {
            lblSesionUsuario.Text = "Sesión iniciada: " + nombreUsuario;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text;
            string password = txtPassword.Text;

            // Le pasamos los datos al presentador, la Vista no toma decisiones
            _presentador.IntentarLogin(correo, password);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text;
            string password = txtPassword.Text;

            // Le pedimos al presentador que registre al usuario con los datos de las cajas de texto
            _presentador.RegistrarUsuario(correo, password);
        }
    }
}
