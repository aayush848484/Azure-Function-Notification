using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace NotificationAzure
{
    public class LogicApp : INotification
    {

        private string endointURL;
        private string content;
        private string email;

        public LogicApp() { }

        public bool setCredentials(Dictionary<string, string> creds)
        {
            try
            {
                this.endointURL = creds["endPointURL"];
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool setMessage(string message)
        {
            try
            {
                this.content = message;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool setReceiver(string receiver)
        {
            try
            {
                this.email = receiver;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private string generatePayLoad()
        {
            if (this.email == null || this.content == null) Console.WriteLine("No data provided"); 
            return "{\"emailAddress\" : \"" + this.email + "\",\"template\": \"" + this.content + "\"}";
        }

        public async Task execute()
        {
            var clientHandler = new HttpClientHandler();
            using (var client = new HttpClient(clientHandler))
            {
                var httpRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(this.endointURL)
                };
                string content = this.generatePayLoad();
                httpRequest.Content = new StringContent(content, Encoding.UTF8, "application/json");
                httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                try
                {
                    HttpResponseMessage response = await client.SendAsync(httpRequest);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error reading contact Info.");
                }
            }
        }
    }
}
