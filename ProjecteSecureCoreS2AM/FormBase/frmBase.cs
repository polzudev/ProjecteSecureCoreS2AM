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
        private readonly List<(ComboBox Cmb, string Columna)> _fkConfigs = new List<(ComboBox, string)>();
        private readonly List<string> _columnasFK = new List<string>();

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

            foreach (string nombre in _columnasFK)
                if (dgvDatos.Columns.Contains(nombre))
                    dgvDatos.Columns.Remove(nombre);
            _columnasFK.Clear();
            _fkConfigs.Clear();

            ConfigurarDGV();
            EnlazarTextBox();
        }
        /// <summary>
        /// Configura la graella de dades després de carregar el <see cref="DataSet"/>.
        /// La implementació per defecte oculta totes les columnes que comencen per "id".
        /// Les subclasses poden sobreescriure aquest mètode per aplicar configuració addicional.
        /// </summary>

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

        /// <summary>
        /// Configura un <see cref="ComboBox"/> per mostrar els valors d'una taula forana i
        /// afegeix una columna descriptiva al <see cref="DataGridView"/>.
        /// Crida aquest mètode des de <see cref="ConfigurarDGV"/> a la subclasse.
        /// </summary>
        protected void ConfigurarFK(ComboBox cmb, string tablaForana, string campoValor, string campoDisplay, string columnaFK)
        {
            DataTable dtForana = bd.PortarDataTable(
                "select " + campoValor + ", " + campoDisplay + " from " + tablaForana);

            cmb.DataSource = dtForana;
            cmb.DisplayMember = campoDisplay;
            cmb.ValueMember = campoValor;

            _fkConfigs.Add((cmb, columnaFK));

            if (dgvDatos.Columns.Contains(columnaFK))
                dgvDatos.Columns[columnaFK].Visible = false;

            string nombreCol = "col_fk_" + columnaFK;
            var colDGV = new DataGridViewComboBoxColumn
            {
                Name = nombreCol,
                HeaderText = campoDisplay,
                DataSource = dtForana,
                DisplayMember = campoDisplay,
                ValueMember = campoValor,
                DataPropertyName = columnaFK,
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                ReadOnly = true
            };
            dgvDatos.Columns.Add(colDGV);
            _columnasFK.Add(nombreCol);
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
            foreach (var (cmb, columna) in _fkConfigs)
            {
                cmb.DataBindings.Clear();
                cmb.DataBindings.Add("SelectedValue", dts.Tables[0], columna);
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
            foreach (var (cmb, _) in _fkConfigs)
            {
                cmb.DataBindings.Clear();
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
            foreach (var (cmb, columna) in _fkConfigs)
            {
                dr[columna] = cmb.SelectedValue ?? (object)DBNull.Value;
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
