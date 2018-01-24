using SafeNav_DLL;
using System.Collections.Generic;

namespace RouteDetails
{
    public class RoutePickingIntelligence
    {
        public List<int> ParseStreetSafety(RouteInformation mapdata)
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
                            counter[i] += Queries.GetAccidentCount(item.stName);
                        }
                    }
                }
                i++;
            }
            return counter;
        }

    }
}