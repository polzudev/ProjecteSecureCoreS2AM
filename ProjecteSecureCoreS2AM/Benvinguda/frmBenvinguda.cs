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
    public partial class frmBenvinguda : Form
    {
       

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
