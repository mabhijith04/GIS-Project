using System;
using System.Net;
using System.Text;

namespace RouteDetails
{
    class HttpRequestReponse
    {
        HttpWebRequest request = null;
        public HttpRequestReponse(string requestUri)
        {
            request = (HttpWebRequest)WebRequest.Create(requestUri);
        }
        
        public string getResponse()
        {
            //return testRead();
            try
            {
                string json = "";
                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    byte[] bytes = Utilities.ReadFully(response.GetResponseStream());
                    json = Encoding.UTF8.GetString(bytes);
                }
                return json;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        //public string testRead()
        //{
        //    return System.IO.File.ReadAllText(@"C:\Users\HP\Desktop\test.txt");
        //}
    }
            
}
