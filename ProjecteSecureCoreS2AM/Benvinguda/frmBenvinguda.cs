using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjecteSecureCoreS2AM.Benvinguda
{
    /// <summary>
    /// Pantalla de benvinguda que mostra el nom i la categoria de l'usuari autenticat i
    /// avança una barra de progrés fins a tancar-se automàticament.
    /// </summary>
    public partial class frmBenvinguda : Form
    {
        /// <summary>
        /// Inicialitza la pantalla de benvinguda amb les dades de l'usuari autenticat.
        /// </summary>
        /// <param name="nomUsuari">Nom complet de l'usuari (columna <c>UserName</c>).</param>
        /// <param name="descCategoria">Descripció de la categoria d'accés de l'usuari (columna <c>DescCategory</c>).</param>
        public frmBenvinguda(string nomUsuari, string descCategoria)
        {
            InitializeComponent();
            lblUsuari.Text = nomUsuari;
            lblCategoria.Text = descCategoria;
        }
        private int max = 5;
        private int contador = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            contador++;
            if (contador>max)
            {
                progressBar1.PerformStep();
            }
            if (progressBar1.Value == progressBar1.Maximum)
            {
                timer1.Stop();
                Close();
            }
        }

        private void frmBenvinguda_Load(object sender, EventArgs e)
        {
          
            timer1.Start();
        }

        private void lblUsuari_Click(object sender, EventArgs e)
        {

        }
    }
}
