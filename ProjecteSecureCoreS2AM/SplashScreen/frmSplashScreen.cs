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
    public partial class frmSplashScreen : Form
    {
        private SplashScreenAudioCode intro;

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
            using (frmLogin login = new frmLogin())
            {
                login.ShowDialog();
            }
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
