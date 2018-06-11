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
            notification.getContactInfo("c488065f-7ce8-4d3b-804d-5796cb4bd75f").Wait();
            notification.execute().Wait();
            Console.ReadLine();
        }
    }
}
