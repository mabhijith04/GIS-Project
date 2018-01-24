using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

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

        public List<Route> GetRoutes()
        {
            return null;
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
            catch(Exception)
            {
                //TODO : write exception
                return null;
            }
        }
    }
}