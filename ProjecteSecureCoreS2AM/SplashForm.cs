using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjecteSecureCoreS2AM
{
    public class SplashForm : Form
    {
        private PictureBox pictureBox;

        public SplashForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(960, 540);
            BackColor = Color.Black;
            TopMost = true;

            pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Black
            };

            string gifPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "commit-jedi-intro.gif");
            if (File.Exists(gifPath))
                pictureBox.Image = Image.FromFile(gifPath);

            Controls.Add(pictureBox);
            Shown += SplashForm_Shown;
        }

        private async void SplashForm_Shown(object sender, EventArgs e)
        {
            string wavPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "intro.wav");
            if (File.Exists(wavPath))
            {
                try { new SoundPlayer(wavPath).Play(); }
                catch { /* audio opcional: si falla, la animación sigue */ }
            }

            await Task.Delay(5400);
            Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                pictureBox?.Image?.Dispose();
            base.Dispose(disposing);
        }
    }
}
