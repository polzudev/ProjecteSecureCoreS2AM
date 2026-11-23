using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProjecteSecureCoreS2AM
{
    /// <summary>
    /// Reprodueix un GIF animat dins un <see cref="PictureBox"/> sincronitzat amb un fitxer
    /// d'àudio WAV, a 60 fotogrames per segon mitjançant un temporitzador de 5 ms.
    /// Cal cridar <see cref="Iniciar"/> des de l'esdeveniment <c>Shown</c> del formulari i
    /// <see cref="Dispose"/> des de <c>FormClosed</c> per alliberar els recursos.
    /// </summary>
    /// <remarks>Generat amb assistència d'IA.</remarks>
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

        /// <summary>
        /// Inicialitza la classe amb el <see cref="PictureBox"/> que mostra el GIF i el flux d'àudio.
        /// </summary>
        /// <param name="pictureBox"><see cref="PictureBox"/> que conté el GIF animat com a imatge.</param>
        /// <param name="audio">Flux d'un fitxer WAV dels recursos del projecte.</param>
        public SplashScreenAudioCode(PictureBox pictureBox, Stream audio)
        {
            this.pictureBox = pictureBox;
            sonido = new SoundPlayer(audio);
            tmrFotogrames.Tick += tmrFotogrames_Tick;
        }

        /// <summary>
        /// Inicia la reproducció simultània del GIF i l'àudio. Cal cridar-lo des de l'esdeveniment
        /// <c>Shown</c> del formulari perquè el <see cref="PictureBox"/> ja sigui visible.
        /// </summary>
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

        /// <summary>
        /// Allibera tots els recursos: atura el temporitzador, el reproductor d'àudio i
        /// restaura la resolució del temporitzador del sistema.
        /// </summary>
        public void Dispose()
        {
            PararFotogramas();
            tmrFotogrames.Dispose();
            sonido.Stop();
            sonido.Dispose();
        }
    }
}
