using System.Data.SqlClient;

namespace SafeNav_DLL
{
    public class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @".\sqlexpress";
            string database = "safenav";
            string username = "sa";
            string password = "SAMPLEPASS";

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }

}