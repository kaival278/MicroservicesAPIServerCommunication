using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
namespace PaymentCardConsumer
{
    public class RestAPICall
    {
       
            private string urlToPost = "";

            public RestAPICall(string urlToPost)
            {
                this.urlToPost = urlToPost;
            }

            public bool postData(WeatherPostModel weatherPostModel)
            {
                WebClient webClient = new WebClient();
                byte[] resByte;
                string resString;
                byte[] reqString;

                try
                {
                    webClient.Headers["content-type"] = "application/json";
                    reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(weatherPostModel, Formatting.Indented));
                    resByte = webClient.UploadData(this.urlToPost, "post", reqString);
                    resString = Encoding.Default.GetString(resByte);
                    Console.WriteLine(resString);
                    webClient.Dispose();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return false;
            }
        
    }
}

