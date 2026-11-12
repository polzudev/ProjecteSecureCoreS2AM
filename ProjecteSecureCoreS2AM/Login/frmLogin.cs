using System;
using System.Data;
using System.Windows.Forms;
using ProjecteSecureCoreS2AM.Benvinguda;
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
            if (login == "" || password == "")
            {
                lblMensaje.Text = "Introdueix l'identificador i la paraula de pas.";
                return;
            }

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
                    return;
                }

                if (usuario != null && PasswordCorrecta(usuario, password))
                {
                    Validar(usuario);
                    return;
                }

                // No se dice si falla el usuario o la contraseña
                lblMensaje.Text = "Identificador o paraula de pas incorrectes.";
                txtPassword.Clear();
                txtPassword.Focus();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No s'ha pogut accedir a la base de dades: " + ex.Message;
            }
        }

        // Primer acceso: escribe la contraseña inicial y en la BD todavía tiene la inicial en claro (sin Salt)
        private bool EsPrimerAcceso(DataRow usuario, string password)
        {
            return password == GestorPasswords.PasswordInicial
                && usuario["Salt"] == DBNull.Value
                && usuario["Password"].ToString() == GestorPasswords.PasswordInicial;
        }

        // Con Salt, la columna Password guarda el Hash: se recalcula y se comparan
        private bool PasswordCorrecta(DataRow usuario, string password)
        {
            if (usuario["Salt"] == DBNull.Value)
            {
                return false;
            }
            try
            {
                byte[] salt = GestorPasswords.ABytes(usuario["Salt"].ToString());
                byte[] hash = GestorPasswords.ABytes(usuario["Password"].ToString());
                return GestorPasswords.VerificarPassword(password, salt, hash);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool CambiarPasswordInicial(DataRow usuario)
        {
            using (frmCambioPassword frm = new frmCambioPassword())
            {
                if (frm.ShowDialog(this) != DialogResult.OK)
                {
                    return false;
                }
                byte[] salt = GestorPasswords.GenerarSalt();
                byte[] hash = GestorPasswords.CalcularHash(frm.NuevaPassword, salt);
                bd.GuardarPassword((int)usuario["idUser"], GestorPasswords.ATexto(salt), GestorPasswords.ATexto(hash));
                return true;
            }
        }

        private void Validar(DataRow usuario)
        {
            UsuarioValidado = usuario;
            DialogResult = DialogResult.OK;
            Hide();
            using (frmBenvinguda benvinguda = new frmBenvinguda(usuario["UserName"].ToString(), usuario["DescCategory"].ToString()))
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
    }
}
