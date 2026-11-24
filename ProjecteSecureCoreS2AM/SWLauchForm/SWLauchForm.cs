using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ProjecteSecureCoreS2AM
{
    public partial class SWLauchForm : UserControl
    {
        private static readonly Color ColorBorde = Color.FromArgb(45, 62, 76);
        private static readonly Color ColorTexto = Color.FromArgb(238, 241, 248);
        private static readonly Color ColorResaltado = Color.FromArgb(242, 194, 48);

        private bool resaltado;
        private bool mostrarImagen = true;
        private bool mostrarPalabraClave;
        private string palabraClave;

        public SWLauchForm()
        {
            InitializeComponent();
            SuscribirEventos(pnlBase);
            pnlBase.Resize += (s, e) => pnlBase.Invalidate();
        }

        [Category("Secure Core"), Description("Nom de l'ensamblat (dll) que conté el formulari")]
        public string Classe { get; set; }

        [Category("Secure Core"), Description("Formulari a obrir en format namespace.NomClasse")]
        public string Form { get; set; }

        [Category("Secure Core"), Description("Text que es mostra a la targeta")]
        public string Texto
        {
            get { return lblTexto.Text; }
            set { lblTexto.Text = value; }
        }

        [Category("Secure Core"), Description("Imatge de la targeta")]
        public Image Imagen
        {
            get { return picIcono.Image; }
            set { picIcono.Image = value; }
        }

        [Category("Secure Core"), Description("Indica si la targeta mostra la imatge"), DefaultValue(true)]
        public bool MostrarImagen
        {
            get { return mostrarImagen; }
            set
            {
                mostrarImagen = value;
                picIcono.Visible = value;
                lblTexto.Dock = value ? DockStyle.Bottom : DockStyle.Fill;
            }
        }

        [Category("Secure Core"), Description("Paraula clau que obre l'opció per veu")]
        public string PalabraClave
        {
            get { return palabraClave; }
            set
            {
                palabraClave = value;
                lblClave.Text = "Veu: \"" + value + "\"";
            }
        }

        [Category("Secure Core"), Description("Indica si la targeta mostra la paraula clau de veu"), DefaultValue(false)]
        public bool MostrarPalabraClave
        {
            get { return mostrarPalabraClave; }
            set
            {
                mostrarPalabraClave = value;
                lblClave.Visible = value;
            }
        }

        public void Ejecutar()
        {
            string rutaDll = Path.Combine(Application.StartupPath, Classe + ".dll");
            if (!File.Exists(rutaDll))
            {
                MessageBox.Show("L'opció \"" + Texto + "\" encara no està disponible.", "Secure Core",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    Assembly ensamblat = Assembly.LoadFrom(rutaDll);
                    Type tipus = ensamblat.GetType(Form);
                    object dllBD = Activator.CreateInstance(tipus);
                    ((System.Windows.Forms.Form)dllBD).Show(FindForm());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No s'ha pogut obrir \"" + Texto + "\": " + ex.Message, "Secure Core",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SuscribirEventos(Control control)
        {
            control.Click += (s, e) => Ejecutar();
            control.MouseEnter += (s, e) => Resaltar(true);
            control.MouseLeave += (s, e) => Resaltar(ClientRectangle.Contains(PointToClient(Cursor.Position)));
            foreach (Control hijo in control.Controls)
            {
                SuscribirEventos(hijo);
            }
        }

        private void Resaltar(bool valor)
        {
            resaltado = valor;
            lblTexto.ForeColor = valor ? ColorResaltado : ColorTexto;
            pnlBase.Invalidate();
        }

        private void pnlBase_Paint(object sender, PaintEventArgs e)
        {
            using (Pen borde = new Pen(resaltado ? ColorResaltado : ColorBorde, 2))
            {
                e.Graphics.DrawRectangle(borde, 1, 1, pnlBase.Width - 2, pnlBase.Height - 2);
            }
        }
    }
}
