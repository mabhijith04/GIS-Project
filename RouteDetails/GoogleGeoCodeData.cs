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
        public GoogleGeoCodeData(string RequestUri)
        {
            this.uri = RequestUri;
        }
        public List<string> GetStreetInfo()
        {
            try
            {
                HttpRequestReponse google = new HttpRequestReponse(uri);
                string json = google.getResponse();
                RootObject mapdata = JsonConvert.DeserializeObject<RootObject>(json);

                HashSet<string> streets = new HashSet<string>();
                foreach (var route in mapdata.routes)
                {
                    foreach (var leg in route.legs)
                    {
                        foreach (var step in leg.steps)
                        {
                            streets.UnionWith(Utilities.GetStreets(step.html_instructions));
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