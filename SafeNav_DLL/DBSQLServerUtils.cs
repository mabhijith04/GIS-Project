using System;
using System.Data.SqlClient;
using System.Configuration;

namespace SafeNav_DLL
{
    class DBSQLServerUtils
    {

        public static SqlConnection GetDBConnection(string datasource, string database, string username, string password)
        {
            string connString = String.Format(ConfigurationManager.ConnectionStrings["SafeNavConnectionString"].ConnectionString,datasource, database, username, password);
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }


    }
}