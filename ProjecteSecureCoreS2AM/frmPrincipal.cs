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
        public frmPrincipal()
        {
            InitializeComponent();
        }
   

        private void timer2_Tick(object sender, EventArgs e)
        {
           
          
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            timer1.Start();
            
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            //Poner la dll con reflection del formulario o el formulario directo
            //Parte del sergi
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
