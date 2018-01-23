using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RouteDetails
{
    public class StreetDetails
    {
        public string stName { get; set; }
        public Step step {get; set;}
    }
    class Utilities
    {
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static List<StreetDetails> GetStreets(Step step)
        {
            string html = step.html_instructions;
            string[] directions = { "north", "south", "east", "west", "northeast", "northwest",
                                    "southeast", "southwest", "left", "right", "straight" };
            List<StreetDetails> streets = new List<StreetDetails>();
            var a = html.Split(new string[] { "<b>" }, StringSplitOptions.None);
            for (int i = 1; i < a.Length; i++)
            {
                StreetDetails sd = new StreetDetails();
                var b = a[i].Split(new string[] { "</b>" }, StringSplitOptions.None)[0];
                if (directions.Contains(b))
                    continue;
                else
                {
                    sd.stName = b;
                    sd.step = step;
                    streets.Add(sd);
                }
            }
            return streets;
        }

    }
}
