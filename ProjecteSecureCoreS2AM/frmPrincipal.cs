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
    public partial class frmPrincipal : Form
    {
        private SplashScreenAudioCode intro;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            ClientSize = pictureBox1.Image.Size;
            CenterToScreen();

            timer1.Start();


        }

        private void frmPrincipal_Shown(object sender, EventArgs e)
        {
            intro = new SplashScreenAudioCode(pictureBox1, Properties.Resources.commit_jedi_splash_audio);
            intro.Iniciar();
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            intro?.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            //Poner la dll con reflection del formulario o el formulario directo
            //Parte del sergi
        }
    }
}
