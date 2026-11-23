using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjecteSecureCoreS2AM
{
    /// <summary>
    /// Pantalla de presentació (splash screen) que reprodueix un GIF animat amb àudio i,
    /// en acabar, inicia el flux d'autenticació: mostra <c>frmLogin</c> i, si és correcte,
    /// <c>frmPrincipal</c>. Si l'usuari tanca la sessió, el bucle torna a demanar credencials.
    /// </summary>
    public partial class frmSplashScreen : Form
    {
        private SplashScreenAudioCode intro;

        /// <summary>Inicialitza el formulari de presentació.</summary>
        public frmSplashScreen()
        {
            InitializeComponent();
        }

        private void frmSplashScreen_Load(object sender, EventArgs e)
        {
            ClientSize = pictureBox1.Image.Size;
            CenterToScreen();

            timer1.Start();


        }

        private void frmSplashScreen_Shown(object sender, EventArgs e)
        {
            intro = new SplashScreenAudioCode(pictureBox1, Properties.Resources.commit_jedi_splash_audio);
            intro.Iniciar();
        }

        private void frmSplashScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            intro?.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Hide();
            // Login -> pantalla principal. Si el usuario cierra sesión se vuelve a pedir el login
            bool mostrarLogin = true;
            while (mostrarLogin)
            {
                mostrarLogin = false;
                using (frmLogin login = (frmLogin)CreadorFormularios.Crear("ProjecteSecureCoreS2AM.frmLogin"))
                {
                    if (login.ShowDialog() == DialogResult.OK)
                    {
                        using (frmPrincipal principal = (frmPrincipal)CreadorFormularios.Crear("ProjecteSecureCoreS2AM.frmPrincipal", login.UsuarioValidado))
                        {
                            principal.ShowDialog();
                            mostrarLogin = principal.SesionCerrada;
                        }
                    }
                }
            }
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
