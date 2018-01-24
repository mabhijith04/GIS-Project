using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SafeNav_DLL;

namespace GoogleApiResponse
{
    class Program
    {
        public static string getRequestUri()
        {
            string origin = "Malleshwaram";
            string destination = "IIITBangalore";
            string key = "AIzaSyAQPgUWeOz_y8BjEjuip3GjLnln3zOUmF8";
            string alternatives = "true";
            string requestUri = "https://maps.googleapis.com/maps/api/directions/json?" +
                                 "origin=" + origin +
                                 "&destination=" + destination +
                                 "&alternatives=" + alternatives +
                                 "&key=" + key;
            
            return requestUri.Replace(" ", "+");

        }

        static void Main(string[] args)
        {
            RouteDetailsService.RouteDetailsServiceClient client = new RouteDetailsService.RouteDetailsServiceClient();
            var streets = client.GetStreetData(getRequestUri());

            SqlConnection conn = DBUtils.GetDBConnection();
            string sql = "insert into AccidentArea([StartLat], [StartLon], [EndLat], [EndLon], [StreetName], [AccidentCount]) values (@slat, @slon, @elat, @elon, @stname, @accidentRate)";
            conn.Open();
            Random random = new Random();
            foreach (var item in streets)
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    int randomNumber = random.Next(0, 100);
                    cmd.Parameters.AddWithValue("@slat", item.step.start_location.lat);
                    cmd.Parameters.AddWithValue("@slon", item.step.start_location.lng);
                    cmd.Parameters.AddWithValue("@elat", item.step.end_location.lat);
                    cmd.Parameters.AddWithValue("@elon", item.step.end_location.lng);
                    cmd.Parameters.AddWithValue("@stname", item.stName);
                    cmd.Parameters.AddWithValue("@accidentRate", randomNumber);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("DONE");
            Console.ReadLine();

        }
    }
}
