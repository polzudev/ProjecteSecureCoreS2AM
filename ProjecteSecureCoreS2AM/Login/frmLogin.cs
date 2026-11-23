using System;
using System.Data;
using System.Windows.Forms;
using ProjecteSecureCoreS2AM.Benvinguda;
namespace ProjecteSecureCoreS2AM
{
    /// <summary>
    /// Formulari d'inici de sessió. Valida les credencials de l'usuari contra la base de
    /// dades, gestiona el primer accés (canvi de la paraula de pas inicial) i controla el
    /// nombre d'intents fallits. En autenticar-se correctament, mostra la pantalla de
    /// benvinguda i estableix <see cref="UsuarioValidado"/>.
    /// </summary>
    public partial class frmLogin : Form
    {
        private readonly UsuariosBD bd = new UsuariosBD();

        /// <summary>
        /// Fila de la base de dades de l'usuari autenticat, disponible quan el diàleg es tanca
        /// amb <see cref="System.Windows.Forms.DialogResult.OK"/>.
        /// </summary>
        public DataRow UsuarioValidado { get; private set; }
        int intents = 0;
        /// <summary>Inicialitza el formulari d'inici de sessió.</summary>
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                lblMensaje.Text = "Introdueix el Username i la paraula de pas.";
            }
            else
            {
                IniciarSesion(login, password);
            }
        }

        private void IniciarSesion(string login, string password)
        {
            try
            {
                DataRow usuario = bd.ObtenerUsuario(login);
                if (usuario != null && EsPrimerAcceso(usuario, password))
                {
                    if (CambiarPasswordInicial(usuario))
                    {
                        Validar(usuario);
                    }
                    else
                    {
                        lblMensaje.Text = "Cal canviar la paraula de pas inicial per poder entrar.";
                        txtPassword.Clear();
                    }
                }
                else if (usuario != null && PasswordCorrecta(usuario, password))
                {
                    Validar(usuario);
                }
                else
                {
                    intents++;
                    lblMensaje.Text = "Identificador o paraula de pas incorrectes.";
                    controlIntents(intents);
                    txtPassword.Clear();
                    txtPassword.Focus();

                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No s'ha pogut accedir a la base de dades: " + ex.Message;
            }
        }

        private bool EsPrimerAcceso(DataRow usuario, string password)
        {
            return password == GestorPasswords.PasswordInicial
                && usuario["Salt"] == DBNull.Value
                && usuario["Password"].ToString() == GestorPasswords.PasswordInicial;
        }

        private bool PasswordCorrecta(DataRow usuario, string password)
        {
            bool correcta = false;
            if (usuario["Salt"] != DBNull.Value)
            {
                byte[] salt = GestorPasswords.ABytes(usuario["Salt"].ToString());
                byte[] hash = GestorPasswords.ABytes(usuario["Password"].ToString());
                correcta = GestorPasswords.VerificarPassword(password, salt, hash);
            }
            return correcta;
        }

        private bool CambiarPasswordInicial(DataRow usuario)
        {
            bool cambiada = false;
            using (frmCambioPassword frm = (frmCambioPassword)CreadorFormularios.Crear("ProjecteSecureCoreS2AM.frmCambioPassword"))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    byte[] salt = GestorPasswords.GenerarSalt();
                    byte[] hash = GestorPasswords.CalcularHash(frm.NuevaPassword, salt);
                    bd.GuardarPassword((int)usuario["idUser"], GestorPasswords.ATexto(salt), GestorPasswords.ATexto(hash));
                    cambiada = true;
                }
            }
            return cambiada;
        }

        private void Validar(DataRow usuario)
        {
            UsuarioValidado = usuario;
            DialogResult = DialogResult.OK;
            Hide();
            using (Form benvinguda = CreadorFormularios.Crear("ProjecteSecureCoreS2AM.Benvinguda.frmBenvinguda", usuario["UserName"].ToString(), usuario["DescCategory"].ToString()))
            {
                benvinguda.ShowDialog();
            }
            Close();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void frmLogin_Load(object sender, EventArgs e) { }

        private void controlIntents(int intents)
        {
            if (intents >= 3)
            {
                MessageBox.Show("Numero D'intents " + intents + " Compte, Intent d'Intrusió!");
            }
        }
    }

}
