using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using SafeNav_DLL;

namespace RouteDetails
{
    public class GoogleGeoCodeData
    {
        string uri = null;
        string json = null;

        public GoogleGeoCodeData(string RequestUri)
        {
            this.uri = RequestUri;
            HttpRequestReponse google = new HttpRequestReponse(uri);
            json = google.getResponse();
        }

        public List<StreetDetails> GetStreetInfo()
        {
            RouteInformation mapdata = JsonConvert.DeserializeObject<RouteInformation>(json);
            return ParseStreetInfo(mapdata);
        }

        public void ReverseGeoCode()
        {

        }

        public List<int> GetRouteSafety()
        {
            RouteInformation mapdata = JsonConvert.DeserializeObject<RouteInformation>(json);
            return ParseStreetSafety(mapdata);
        }

        private List<int> ParseStreetSafety(RouteInformation mapdata)
        {
            HashSet<StreetDetails> streets = new HashSet<StreetDetails>();
            int i = 0;
            List<int> counter = new List<int>();
            foreach (var route in mapdata.routes)
            {
                counter.Add(0);
                foreach (var leg in route.legs)
                {
                    foreach (var step in leg.steps)
                    {
                        List<StreetDetails> stNames = Utilities.GetStreets(step);
                        foreach (var item in stNames)
                        {
                            counter[i] += GetAccidentCount(item.stName);
                        }
                    }
                }
                i++;
            }
            return counter;
        }

        int GetAccidentCount(string stName)
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

        public List<StreetDetails> ParseStreetInfo(RouteInformation mapdata)
        {
            try
            {
                HashSet<StreetDetails> streets = new HashSet<StreetDetails>();
                foreach (var route in mapdata.routes)
                {
                    foreach (var leg in route.legs)
                    {
                        foreach (var step in leg.steps)
                        {
                            streets.UnionWith(Utilities.GetStreets(step));
                        }
                    }
                }
                return streets.ToList();
            }
            catch (Exception)
            {
                //TODO : write exception
                return null;
            }
        }
    }
}