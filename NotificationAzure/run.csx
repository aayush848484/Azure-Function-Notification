using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    Notification notification = new Notification();
    notification.getContactInfo("c488065f-7ce8-4d3b-804d-5796cb4bd75f").Wait();
    notification.execute().Wait();
}