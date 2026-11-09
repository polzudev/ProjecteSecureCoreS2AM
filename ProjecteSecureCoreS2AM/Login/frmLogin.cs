using System;
using System.Data;
using System.Windows.Forms;

namespace ProjecteSecureCoreS2AM
{
    public partial class frmLogin : Form
    {
        private readonly UsuariosBD bd = new UsuariosBD();

        public DataRow UsuarioValidado { get; private set; }

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
                    lblMensaje.Text = "Identificador o paraula de pas incorrectes.";
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
            using (frmCambioPassword frm = new frmCambioPassword())
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
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
