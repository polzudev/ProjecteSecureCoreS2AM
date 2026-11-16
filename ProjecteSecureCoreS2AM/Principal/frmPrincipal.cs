using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ProjecteSecureCoreS2AM
{
    public partial class frmPrincipal : Form
    {
        private readonly PrincipalBD bd = new PrincipalBD();
        private readonly List<SWLauchForm> lanzadores = new List<SWLauchForm>();
        private readonly int idUsuario;
        private readonly Image imagenPorDefecto = Properties.Resources.logo_CommitJedi_fondo_transparente;
        private DataRow perfil;
        private int nivelAcceso;

        public bool SesionCerrada { get; private set; }

        public frmPrincipal(DataRow usuario)
        {
            InitializeComponent();
            idUsuario = (int)usuario["idUser"];
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                CargarPerfil();
                CargarOpciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No s'ha pogut accedir a la base de dades: " + ex.Message, "Secure Core",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            lblEstado.Text = "Sessió iniciada a les " + DateTime.Now.ToString("HH:mm");
            MostrarHora();
            tmrReloj.Start();
        }

        private void CargarPerfil()
        {
            perfil = bd.ObtenerPerfil(idUsuario);
            if (perfil != null)
            {
                nivelAcceso = perfil["AccessLevel"] == DBNull.Value ? 0 : Convert.ToInt32(perfil["AccessLevel"]);
                lblUsuario.Text = perfil["UserName"].ToString();
                lblCategoria.Text = perfil["DescCategory"] + "  ·  Nivell d'accés " + nivelAcceso;
                lblRango.Text = "Rang: " + perfil["DescRank"] + "  ·  " + perfil["CodeUser"];
            }
        }

        private void CargarOpciones()
        {
            flpOpciones.Controls.Clear();
            mnuOpciones.DropDownItems.Clear();
            lanzadores.Clear();

            foreach (DataRow fila in bd.ObtenerOpciones(nivelAcceso).Rows)
            {
                SWLauchForm lanzador = CrearLanzador(fila);
                lanzadores.Add(lanzador);
                flpOpciones.Controls.Add(lanzador);
                mnuOpciones.DropDownItems.Add(CrearItemMenu(lanzador));
            }

            lblSeccion.Text = "OPCIONS DISPONIBLES (" + lanzadores.Count + ")";
            mnuOpciones.Enabled = lanzadores.Count > 0;
        }

        private SWLauchForm CrearLanzador(DataRow fila)
        {
            string espacioNombres = fila["namespace"].ToString();
            string formulario = fila["formName"].ToString();

            SWLauchForm lanzador = new SWLauchForm();
            lanzador.Texto = fila["description"].ToString();
            lanzador.Classe = espacioNombres;
            lanzador.Form = string.IsNullOrEmpty(espacioNombres) ? formulario : espacioNombres + "." + formulario;
            lanzador.Imagen = CargarImagen(fila["image"].ToString());
            return lanzador;
        }

        private ToolStripMenuItem CrearItemMenu(SWLauchForm lanzador)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(lanzador.Texto, lanzador.Imagen);
            item.Click += (s, e) => lanzador.Ejecutar();
            return item;
        }

        private Image CargarImagen(string ruta)
        {
            Image imagen = imagenPorDefecto;
            string rutaCompleta = string.IsNullOrEmpty(ruta) ? "" : Path.Combine(Application.StartupPath, ruta);
            if (File.Exists(rutaCompleta))
            {
                imagen = Image.FromFile(rutaCompleta);
            }
            return imagen;
        }

        private void MostrarInfoUsuario()
        {
            string info = "No s'han pogut carregar les dades de l'usuari.";
            if (perfil != null)
            {
                info = "Nom:  " + perfil["UserName"]
                     + "\nCodi:  " + perfil["CodeUser"]
                     + "\nLogin:  " + perfil["Login"]
                     + "\nCategoria:  " + perfil["DescCategory"] + " (" + perfil["CodeCategory"] + ")"
                     + "\nNivell d'accés:  " + nivelAcceso
                     + "\nRang:  " + perfil["DescRank"]
                     + "\nOpcions disponibles:  " + lanzadores.Count;
            }
            MessageBox.Show(info, "Informació de l'usuari", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CerrarSesion()
        {
            SesionCerrada = true;
            Close();
        }

        private void MostrarHora()
        {
            lblHora.Text = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
        }

        private void tmrReloj_Tick(object sender, EventArgs e)
        {
            MostrarHora();
        }

        private void mnuInfoUsuario_Click(object sender, EventArgs e)
        {
            MostrarInfoUsuario();
        }

        private void mnuCerrarSesion_Click(object sender, EventArgs e)
        {
            CerrarSesion();
        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            tmrReloj.Stop();
        }

        private void pnlCabecera_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblSeccion_Click(object sender, EventArgs e)
        {

        }
    }
}
