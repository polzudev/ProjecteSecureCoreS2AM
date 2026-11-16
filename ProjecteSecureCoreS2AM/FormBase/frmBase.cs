using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ProjecteSecureCoreS2AM
{
    public partial class frmBase : Form
    {
        private AccesoDatos bd;
        private DataSet dts;
        private bool esNuevo;

        [Category("Secure Core"), Description("Nom de la taula de la base de dades a mantenir")]
        public string Tabla { get; set; }

        private string Consulta
        {
            get { return "select * from " + Tabla; }
        }

        public frmBase()
        {
            InitializeComponent();
        }

        private void frmBase_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Text = Tabla;
                lblTitulo.Text = "Manteniment de " + Tabla;
                try
                {
                    bd = new AccesoDatos();
                    CargarDatos();
                }
                catch (Exception ex)
                {
                    btnNuevo.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnEliminar.Enabled = false;
                    MostrarError("No s'han pogut carregar les dades", ex);
                }
            }
        }

        private void CargarDatos()
        {
            dts = bd.PortarTaula(Tabla);
            dgvDatos.DataSource = dts.Tables[0];
            ConfigurarDGV();
            EnlazarTextBox();
        }

        protected virtual void ConfigurarDGV()
        {
            foreach (DataGridViewColumn col in dgvDatos.Columns)
            {
                if (col.Name.StartsWith("id"))
                {
                    col.Visible = false;
                }
            }
        }

        private IEnumerable<SWTextBox> CamposEnlazados()
        {
            return Controls.OfType<SWTextBox>().Where(txt => !string.IsNullOrEmpty(txt.CampoBD));
        }

        private void EnlazarTextBox()
        {
            foreach (SWTextBox txt in CamposEnlazados())
            {
                txt.DataBindings.Clear();
                txt.DataBindings.Add("Text", dts.Tables[0], txt.CampoBD);
                txt.Validated -= ValidarTextBox;
                txt.Validated += ValidarTextBox;
            }
        }

        private void DesenlazarTextBox()
        {
            foreach (SWTextBox txt in CamposEnlazados())
            {
                txt.DataBindings.Clear();
                txt.Text = "";
                txt.Validated -= ValidarTextBox;
            }
        }

        private void ValidarTextBox(object sender, EventArgs e)
        {
            TextBox ctr = (TextBox)sender;
            if (ctr.DataBindings.Count > 0)
            {
                ctr.DataBindings[0].BindingManagerBase.EndCurrentEdit();
            }
        }

        private void CambiarModoInsercion(bool insercion)
        {
            esNuevo = insercion;
            dgvDatos.Enabled = !insercion;
            btnNuevo.Enabled = !insercion;
            btnEliminar.Enabled = !insercion;
            btnCancelar.Enabled = insercion;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            CambiarModoInsercion(true);
            DesenlazarTextBox();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CambiarModoInsercion(false);
            EnlazarTextBox();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            List<string> incorrectos = CamposIncorrectos();
            if (incorrectos.Count > 0)
            {
                MessageBox.Show("Revisa aquests camps: " + string.Join(", ", incorrectos), Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Guardar();
            }
        }

        private List<string> CamposIncorrectos()
        {
            List<string> incorrectos = new List<string>();
            foreach (SWTextBox txt in CamposEnlazados())
            {
                if (!txt.ValidateData())
                {
                    incorrectos.Add(txt.CampoBD);
                }
            }
            return incorrectos;
        }

        private void Guardar()
        {
            try
            {
                if (esNuevo)
                {
                    AgregarFila();
                }
                else
                {
                    BindingContext[dts.Tables[0]].EndCurrentEdit();
                }
                bd.Actualitzar(Consulta, dts);
                CambiarModoInsercion(false);
                CargarDatos();
            }
            catch (Exception ex)
            {
                dts.RejectChanges();
                MostrarError("No s'han pogut desar els canvis", ex);
            }
        }

        private void AgregarFila()
        {
            DataRow dr = dts.Tables[0].NewRow();
            foreach (SWTextBox txt in CamposEnlazados())
            {
                dr[txt.CampoBD] = txt.Text == "" ? (object)DBNull.Value : txt.Text;
            }
            dts.Tables[0].Rows.Add(dr);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.CurrentRow != null && ConfirmarEliminacion())
            {
                try
                {
                    DataRowView fila = (DataRowView)dgvDatos.CurrentRow.DataBoundItem;
                    fila.Row.Delete();
                    bd.Actualitzar(Consulta, dts);
                    CargarDatos();
                }
                catch (Exception ex)
                {
                    dts.RejectChanges();
                    MostrarError("No s'ha pogut eliminar el registre", ex);
                }
            }
        }

        private bool ConfirmarEliminacion()
        {
            return MessageBox.Show("Vols eliminar el registre seleccionat?", Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void MostrarError(string mensaje, Exception ex)
        {
            MessageBox.Show(mensaje + ": " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
