using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationAzure
{
    public class Twilio : INotification
    {
        public Task execute()
        {
            throw new NotImplementedException();
        }

        public bool setCredentials(Dictionary<string, string> creds)
        {
            throw new NotImplementedException();
        }

        public bool setMessage(string message)
        {
            throw new NotImplementedException();
        }

        public bool setReceiver(string receivers)
        {
            throw new NotImplementedException();
        }
    }
}
