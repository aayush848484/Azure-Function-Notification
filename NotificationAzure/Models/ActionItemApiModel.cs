using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationAzure
{
    public class ActionItemApiModel
    {
        [JsonProperty(PropertyName = "ActionType")]
        public string ActionType { get; set; } = String.Empty;

        [JsonProperty(PropertyName = "Value")]
        public string Value { get; set; } = String.Empty;

        public ActionItemApiModel() { }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (this.ActionType != null ? this.ActionType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return hashCode;
            }
        }

        public Dictionary<string, string> getActionItemInDictionary()
        {
            return new Dictionary<string, string>()
            {
                {"ActionType", this.ActionType },
                {"Value", this.Value }
            };
        }
    }
}
