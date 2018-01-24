using System.Data.SqlClient;

namespace SafeNav_DLL
{
    public class Queries
    {
        public static int GetAccidentCount(string stName)
        {
            try
            {
                SqlConnection conn = DBUtils.GetDBConnection();
                int count = 0;
                string cmd = "select AccidentCount from AccidentArea where StreetName = @stName";
                SqlCommand oCmd = new SqlCommand(cmd, conn);
                oCmd.Parameters.AddWithValue("@stName", stName);
                conn.Open();
                SqlDataReader dr = oCmd.ExecuteReader();
                dr.Read();
                count += dr.GetInt32(0);
                conn.Close();

                return count;
            }
            catch
            {
                return 0;
            }
        }
    }
}
