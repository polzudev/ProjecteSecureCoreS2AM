using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WinForm = System.Windows.Forms.Form;

namespace ProjecteSecureCoreS2AM
{
    // Botón-tarjeta que abre un formulario por reflection
    public partial class SWLauchForm : UserControl
    {
        private static readonly Color ColorBorde = Color.FromArgb(45, 62, 76);
        private static readonly Color ColorResaltado = Color.FromArgb(242, 194, 48);

        private bool resaltado;

        public SWLauchForm()
        {
            InitializeComponent();
            foreach (Control hijo in Controls)
            {
                hijo.Click += Hijo_Click;
                hijo.MouseEnter += Hijo_MouseEnter;
                hijo.MouseLeave += Hijo_MouseLeave;
            }
        }

        [Category("Secure Core"), Description("Nom de l'ensamblat (dll) que conté el formulari. Buit = aquest executable.")]
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

        public void Ejecutar()
        {
            try
            {
                Type tipo = ObtenerTipo();
                if (tipo == null)
                {
                    MessageBox.Show("L'opció \"" + Texto + "\" encara no està disponible.", "Secure Core",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    WinForm formulario = (WinForm)Activator.CreateInstance(tipo);
                    formulario.Show(FindForm());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No s'ha pogut obrir \"" + Texto + "\": " + ex.Message, "Secure Core",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Si existe la dll indicada en Classe se busca allí; si no, en este mismo ejecutable
        private Type ObtenerTipo()
        {
            Type tipo = null;
            if (!string.IsNullOrEmpty(Form))
            {
                Assembly ensamblado = Assembly.GetExecutingAssembly();
                string rutaDll = string.IsNullOrEmpty(Classe) ? "" : Path.Combine(Application.StartupPath, Classe + ".dll");
                if (File.Exists(rutaDll))
                {
                    ensamblado = Assembly.LoadFrom(rutaDll);
                }

                tipo = ensamblado.GetType(Form);
                if (tipo == null)
                {
                    // Por si el namespace no coincide: se busca solo por el nombre de la clase
                    string nombreClase = Form.Substring(Form.LastIndexOf('.') + 1);
                    tipo = ensamblado.GetTypes().FirstOrDefault(t => t.Name == nombreClase);
                }

                if (tipo != null && !tipo.IsSubclassOf(typeof(WinForm)))
                {
                    tipo = null;
                }
            }
            return tipo;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Ejecutar();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Resaltar(true);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Resaltar(RatonDentro());
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen borde = new Pen(resaltado ? ColorResaltado : ColorBorde, 2))
            {
                e.Graphics.DrawRectangle(borde, 1, 1, Width - 2, Height - 2);
            }
        }

        private void Hijo_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void Hijo_MouseEnter(object sender, EventArgs e)
        {
            Resaltar(true);
        }

        private void Hijo_MouseLeave(object sender, EventArgs e)
        {
            Resaltar(RatonDentro());
        }

        private bool RatonDentro()
        {
            return ClientRectangle.Contains(PointToClient(Cursor.Position));
        }

        private void Resaltar(bool valor)
        {
            if (resaltado != valor)
            {
                resaltado = valor;
                lblTexto.ForeColor = valor ? ColorResaltado : Color.FromArgb(238, 241, 248);
                Invalidate();
            }
        }

        private void picIcono_Click(object sender, EventArgs e)
        {

        }
    }
}
