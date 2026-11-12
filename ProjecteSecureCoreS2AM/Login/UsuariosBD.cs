using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProjecteSecureCoreS2AM
{
    // Acceso a la tabla Users para la validación. Todas las consultas van con parámetros (sin SQL Injection).
    public class UsuariosBD
    {
        private string CadenaConexion
        {
            get
            {
                ConnectionStringSettings conf = ConfigurationManager.ConnectionStrings["SecureCore"];
                if (conf == null)
                {
                    throw new ConfigurationErrorsException("Falta la cadena de connexió 'SecureCore' a App.config");
                }
                return conf.ConnectionString;
            }
        }

        // Devuelve la fila del usuario con ese login, o null si no existe
        public DataRow ObtenerUsuario(string login)
        {
            string query = "select u.idUser, u.CodeUser, u.UserName, u.Login, u.Password, u.Salt, u.idUserCategory, u.idUserRank, uc.DescCategory from Users u left join UserCategories uc on u.idUserCategory = uc.idUserCategory where u.Login = @login";
            using (SqlConnection conn = new SqlConnection(CadenaConexion))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
            {
                adapter.SelectCommand.Parameters.Add("@login", SqlDbType.NVarChar, 12).Value = login;
                DataSet dts = new DataSet();
                conn.Open();
                adapter.Fill(dts, "Users");
                conn.Close();
                DataTable usuarios = dts.Tables["Users"];
                return usuarios.Rows.Count == 1 ? usuarios.Rows[0] : null;
            }
        }

        // Guarda el Salt y, en lugar de la contraseña inicial en claro, su Hash
        public void GuardarPassword(int idUser, string salt, string hash)
        {
            string query = "update Users set Salt = @salt, Password = @hash where idUser = @idUser";
            using (SqlConnection conn = new SqlConnection(CadenaConexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@salt", SqlDbType.NVarChar, 200).Value = salt;
                cmd.Parameters.Add("@hash", SqlDbType.NVarChar, 200).Value = hash;
                cmd.Parameters.Add("@idUser", SqlDbType.Int).Value = idUser;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
