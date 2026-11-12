using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProjecteSecureCoreS2AM
{
    public partial class frmCambioPassword : Form
    {
        private static readonly Color ColorCorrecto = Color.FromArgb(120, 220, 140);
        private static readonly Color ColorError = Color.FromArgb(255, 120, 120);

        public string NuevaPassword => txtNueva.Text;

        public frmCambioPassword()
        {
            InitializeComponent();
            ActualizarEstado();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            ActualizarEstado();
        }

        private void ActualizarEstado()
        {
            List<string> incumplidos = GestorPasswords.RequisitosIncumplidos(txtNueva.Text);
            if (incumplidos.Count == 0)
            {
                lblRequisitos.Text = ":) Compleix tots els requisits";
                lblRequisitos.ForeColor = ColorCorrecto;
            }
            else
            {
                lblRequisitos.Text = string.Join("\n", incumplidos.Select(r => "x " + r));
                lblRequisitos.ForeColor = ColorError;
            }

            bool coinciden = txtRepetir.Text != "" && txtNueva.Text == txtRepetir.Text;
            if (txtRepetir.Text == "")
            {
                lblCoincidencia.Text = "";
            }
            else if (coinciden)
            {
                lblCoincidencia.Text = ":D Les paraules de pas coincideixen";
                lblCoincidencia.ForeColor = ColorCorrecto;
            }
            else
            {
                lblCoincidencia.Text = "x Les paraules de pas no coincideixen";
                lblCoincidencia.ForeColor = ColorError;
            }

            btnGuardar.Enabled = coinciden && incumplidos.Count == 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
