using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProjecteSecureCoreS2AM
{
    /// <summary>
    /// Diàleg de canvi de paraula de pas que valida en temps real els requisits de seguretat
    /// definits a <see cref="GestorPasswords"/> i confirma que les dues entrades coincideixen.
    /// La nova paraula de pas acceptada és accessible a través de <see cref="NuevaPassword"/>.
    /// </summary>
    public partial class frmCambioPassword : Form
    {
        private static readonly Color ColorCorrecto = Color.FromArgb(120, 220, 140);
        private static readonly Color ColorError = Color.FromArgb(255, 120, 120);

        /// <summary>
        /// Paraula de pas nova introduïda per l'usuari, disponible quan el diàleg es tanca amb
        /// <see cref="DialogResult.OK"/>.
        /// </summary>
        public string NuevaPassword => txtNueva.Text;

        /// <summary>Inicialitza el diàleg i actualitza l'estat de validació inicial.</summary>
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
