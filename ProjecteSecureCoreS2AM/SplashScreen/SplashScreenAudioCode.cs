using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProjecteSecureCoreS2AM
{
    // Reproduce un GIF animado en un PictureBox sincronizado con un sonido WAV.
    public class SplashScreenAudioCode : IDisposable
    {
        private const int FotogramasPorSegundo = 60;
        [DllImport("winmm.dll")] private static extern uint timeBeginPeriod(uint ms);
        [DllImport("winmm.dll")] private static extern uint timeEndPeriod(uint ms);

        private readonly PictureBox pictureBox;
        private readonly SoundPlayer sonido;
        private readonly Stopwatch reloj = new Stopwatch();
        private readonly Timer tmrFotogrames = new Timer { Interval = 5 };
        private int totalFotogrames;
        private int fotogramaActual = -1;

        public SplashScreenAudioCode(PictureBox pictureBox, Stream audio)
        {
            this.pictureBox = pictureBox;
            sonido = new SoundPlayer(audio);
            tmrFotogrames.Tick += tmrFotogrames_Tick;
        }

        public void Iniciar()
        {
            totalFotogrames = pictureBox.Image.GetFrameCount(FrameDimension.Time);
            pictureBox.Enabled = false;
            timeBeginPeriod(1);
            sonido.Play();
            reloj.Start();
            tmrFotogrames.Start();
        }

        private void tmrFotogrames_Tick(object sender, EventArgs e)
        {
            int fotograma = (int)(reloj.ElapsedMilliseconds * FotogramasPorSegundo / 1000);
            if (fotograma >= totalFotogrames)
            {
                fotograma = totalFotogrames - 1;
                PararFotogramas();
            }
            if (fotograma == fotogramaActual) return;
            fotogramaActual = fotograma;
            pictureBox.Image.SelectActiveFrame(FrameDimension.Time, fotograma);
            pictureBox.Invalidate();
        }

        private void PararFotogramas()
        {
            if (tmrFotogrames.Enabled)
            {
                tmrFotogrames.Stop();
                timeEndPeriod(1);
            }
        }

        public void Dispose()
        {
            PararFotogramas();
            tmrFotogrames.Dispose();
            sonido.Stop();
            sonido.Dispose();
        }
    }
}
