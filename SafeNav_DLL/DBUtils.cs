using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string password = "Lgp765phone#";

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }

}