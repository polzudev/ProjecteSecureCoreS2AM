using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProjecteSecureCoreS2AM
{
    /// <summary>
    /// Proporciona operacions CRUD genèriques sobre la base de dades SQL Server definida a
    /// la cadena de connexió "SecureCore" de <c>App.config</c>.
    /// </summary>
    public class AccesoDatos
    {
        private SqlConnection conn;

        /// <summary>Crea una nova instància i estableix la connexió amb la base de dades.</summary>
        public AccesoDatos()
        {
            Connectar();
        }

        private void Connectar()
        {
            ConnectionStringSettings conf = ConfigurationManager.ConnectionStrings["SecureCore"];
            if (conf == null)
            {
                throw new ConfigurationErrorsException("Falta la cadena de connexió 'SecureCore' a App.config");
            }
            conn = new SqlConnection(conf.ConnectionString);
        }

        /// <summary>
        /// Retorna totes les files d'una taula com a <see cref="DataSet"/>.
        /// </summary>
        /// <param name="taula">Nom de la taula de la base de dades.</param>
        /// <returns><see cref="DataSet"/> amb una <see cref="DataTable"/> anomenada igual que la taula.</returns>
        public DataSet PortarTaula(string taula)
        {
            return PortarPerConsulta("select * from " + taula, taula);
        }

        /// <summary>
        /// Executa una consulta SELECT i retorna el resultat com a <see cref="DataSet"/> amb la
        /// taula anomenada "Table".
        /// </summary>
        /// <param name="consulta">Consulta SQL SELECT.</param>
        /// <returns><see cref="DataSet"/> amb una <see cref="DataTable"/> anomenada "Table".</returns>
        public DataSet PortarPerConsulta(string consulta)
        {
            return PortarPerConsulta(consulta, "Table");
        }

        /// <summary>
        /// Executa una consulta SELECT i retorna el resultat com a <see cref="DataSet"/> amb un
        /// nom de taula personalitzat.
        /// </summary>
        /// <param name="consulta">Consulta SQL SELECT.</param>
        /// <param name="nomDataTable">Nom que rep la <see cref="DataTable"/> dins el <see cref="DataSet"/>.</param>
        /// <returns><see cref="DataSet"/> amb la taula resultant.</returns>
        public DataSet PortarPerConsulta(string consulta, string nomDataTable)
        {
            return Omplir(consulta, new DataSet(), nomDataTable);
        }

        /// <summary>
        /// Executa una consulta SELECT i retorna directament la primera <see cref="DataTable"/>.
        /// </summary>
        /// <param name="consulta">Consulta SQL SELECT.</param>
        /// <returns>Primera <see cref="DataTable"/> del resultat.</returns>
        public DataTable PortarDataTable(string consulta)
        {
            return PortarPerConsulta(consulta).Tables[0];
        }

        /// <summary>
        /// Afegeix el resultat d'una consulta SELECT com a nova <see cref="DataTable"/> dins un
        /// <see cref="DataSet"/> existent.
        /// </summary>
        /// <param name="consulta">Consulta SQL SELECT.</param>
        /// <param name="dts"><see cref="DataSet"/> al qual s'afegeix la nova taula.</param>
        /// <returns>El mateix <see cref="DataSet"/> amb la nova taula afegida.</returns>
        public DataSet AfegirDataTable(string consulta, DataSet dts)
        {
            return Omplir(consulta, dts, "Table" + dts.Tables.Count);
        }

        /// <summary>
        /// Envia els canvis pendents del <see cref="DataSet"/> a la base de dades usant
        /// <see cref="System.Data.SqlClient.SqlCommandBuilder"/> per generar les sentències INSERT/UPDATE/DELETE.
        /// </summary>
        /// <param name="consulta">Consulta SELECT original que defineix la taula a actualitzar.</param>
        /// <param name="dts"><see cref="DataSet"/> que conté els canvis a persistir.</param>
        /// <returns>Nombre de files afectades, o 0 si no hi ha canvis.</returns>
        public int Actualitzar(string consulta, DataSet dts)
        {
            int registresAfectats = 0;
            using (SqlDataAdapter adapter = new SqlDataAdapter(consulta, conn))
            using (SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter))
            {
                try
                {
                    conn.Open();
                    if (dts.HasChanges())
                    {
                        registresAfectats = adapter.Update(dts.Tables[0]);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            return registresAfectats;
        }

        /// <summary>
        /// Executa una sentència SQL que no retorna files (INSERT, UPDATE, DELETE, DDL).
        /// </summary>
        /// <param name="consulta">Sentència SQL a executar.</param>
        /// <returns>Nombre de files afectades.</returns>
        public int Executa(string consulta)
        {
            int registresAfectats;
            using (SqlCommand cmd = new SqlCommand(consulta, conn))
            {
                try
                {
                    conn.Open();
                    registresAfectats = cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
            return registresAfectats;
        }

        /// <summary>
        /// Executa un Stored Procedure i retorna el resultat com a <see cref="DataTable"/>.
        /// </summary>
        /// <param name="nomSP">Nom del Stored Procedure a executar.</param>
        /// <param name="parametres">Paràmetres del SP com a diccionari nom→valor. Pot ser null.</param>
        /// <returns><see cref="DataTable"/> amb el resultat retornat pel SP.</returns>
        public DataTable ExecutarSP(string nomSP, Dictionary<string, object> parametres = null)
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(nomSP, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (parametres != null)
                {
                    foreach (var p in parametres)
                    {
                        if (p.Value == null)
                            cmd.Parameters.AddWithValue(p.Key, DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue(p.Key, p.Value);
                    }
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    try
                    {
                        conn.Open();
                        adapter.Fill(dt);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return dt;
        }

        private DataSet Omplir(string consulta, DataSet dts, string nomDataTable)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(consulta, conn))
            {
                try
                {
                    conn.Open();
                    adapter.Fill(dts, nomDataTable);
                }
                finally
                {
                    conn.Close();
                }
            }
            return dts;
        }
        public void ExecutarTransaccio(List<string> consultes)
        {
            conn.Open();
            SqlTransaction transaccio = conn.BeginTransaction();
            try
            {
                foreach (string consulta in consultes)
                {
                    SqlCommand cmd = new SqlCommand(consulta, conn, transaccio);
                    cmd.ExecuteNonQuery();
                }
                transaccio.Commit();
            }
            catch
            {
                transaccio.Rollback(); 
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
