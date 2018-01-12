﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoogleApiResponse
{
    class Program
    {
        public static string getRequestUri()
        {
            string origin = "Electronics City";
            string destination = "RT Nagar";
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
            foreach (var item in streets)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}