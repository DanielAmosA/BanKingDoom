using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Server.Helpers.WebEnum
{
    /// <summary>
    /// The enum responsible for represents the Type of transaction.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TransactionTypes
    {
        Deposit = 0,
        Withdrawal = 1
    }
}
