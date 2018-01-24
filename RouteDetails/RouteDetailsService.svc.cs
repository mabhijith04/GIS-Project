using System.Collections.Generic;

namespace RouteDetails
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class RouteDetailsService : IRouteDetailsService
    {
        public List<StreetDetails> GetStreetData(string requestUri)
        {
            GoogleGeoCodeData google = new GoogleGeoCodeData(requestUri);
            return google.GetStreetInfo();
        }

        public List<int> GetRouteSafety(string requestUri)
        {
            GoogleGeoCodeData google = new GoogleGeoCodeData(requestUri);
            return google.GetRouteSafety();
        }
    }
}
