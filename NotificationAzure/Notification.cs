using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


public interface INotification
{
    Boolean setReceiver(string receivers);
    Boolean setMessage(string message);
    Boolean setCredentials(Dictionary<string, string> creds);
    Task execute();
}

namespace NotificationAzure
{
    public class Notification
    {

        private const EmailImplementationTypes EMAIL_IMPLEMENTATION_TYPE = EmailImplementationTypes.LogicApp;
        private const SMSImplementationTypes SMS_IMPLEMENTATION_TYPE = SMSImplementationTypes.Twilio;
        private const string TELEMETRY_ENDPOINT_URL = @"http://localhost:9004/v1/rules/";

        public IList<ActionItemApiModel> actionList;
        public INotification implementation;


        // Mapping to the Implementation type based on the setup.
        IDictionary<EmailImplementationTypes, Func<INotification>> actionsEmail = new Dictionary<EmailImplementationTypes, Func<INotification>>(){
        {EmailImplementationTypes.LogicApp, () =>  new LogicApp()},
        {EmailImplementationTypes.SendGrid, () =>  new SendGrid()}
        };

        IDictionary<SMSImplementationTypes, Func<INotification>> smsEmail = new Dictionary<SMSImplementationTypes, Func<INotification>>(){
        {SMSImplementationTypes.Twilio, () => new Twilio()}
        };

        public Notification() { }

        public async Task execute()
        {

            foreach (ActionItemApiModel action in this.actionList)
            {
                if (action.ActionType == "Email")
                {
                    implementation = actionsEmail[EMAIL_IMPLEMENTATION_TYPE]();
                    var credentialDictionary = new Dictionary<string, string>();
                    credentialDictionary.Add("endPointURL", @"https://prod-08.southeastasia.logic.azure.com:443/workflows/eda09bfd1c8a4654a88d016d612c433f/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=O7Ru1e-spey8dWfbCHRwwPcWCLXaiVBBRMKiBEqzkbo");
                    implementation.setCredentials(credentialDictionary);
                }
                else if (action.ActionType == "SMS")
                {
                    implementation = smsEmail[SMS_IMPLEMENTATION_TYPE]();
                }
                implementation.setMessage("Hey this is me.");
                implementation.setReceiver(action.Value);
                await implementation.execute();
            }
        }

        public async Task<bool> getContactInfo(string ruleID)
        {

            var clientHandler = new HttpClientHandler();
            using (var client = new HttpClient(clientHandler))
            {
                var httpRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(TELEMETRY_ENDPOINT_URL + ruleID)
                };

                try
                {
                    using (var response = await client.SendAsync(httpRequest))
                    {
                        RuleApiModel rule = JsonConvert.DeserializeObject<RuleApiModel>(await response.Content.ReadAsStringAsync());
                        this.actionList = rule.Actions;
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error reading contact Info.");
                    return false;
                }
            }


        }
    }
}

enum EmailImplementationTypes
{
    LogicApp,
    SendGrid,
}

enum SMSImplementationTypes
{
    Twilio
}

