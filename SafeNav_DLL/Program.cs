using System;
using SafeNav_DLL;
using System.Data.SqlClient;

namespace ConnectSQLServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting Connection ...");
            SqlConnection conn = DBUtils.GetDBConnection();

            try
            {
                Console.WriteLine("Openning Connection ...");

                conn.Open();

                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Console.Read();
        }
    }

}