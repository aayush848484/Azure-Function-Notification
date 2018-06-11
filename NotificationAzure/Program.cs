using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationAzure
{
    class Program
    {
        static void Main(string[] args)
        {
            Notification notification = new Notification();
            notification.getContactInfo("0e3ac379-3e27-44c3-a43c-07e9c3f56b88").Wait();
            notification.execute().Wait();
        }
    }
}
