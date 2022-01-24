using System;
using System.Text.Json.Serialization;

namespace Core
{
    public class Transaction
    {
        [JsonPropertyName("id")]
        public String TransactionId { get; set; }
        
        [JsonIgnore]
        public decimal Amount { get; set; }
        [JsonIgnore]
        public String CurrencyCode { get; set; }
        [JsonIgnore]
        public DateTime TransactionDate { get; set; }

        [JsonPropertyName("payment")]
        public String Payment
        {
            get
            {
                return this.Amount.ToString() +" "+ this.CurrencyCode;
            }
        }

        [JsonIgnore]
        public String Status { get; set; }

        [JsonPropertyName("status")]
        public String ShortStatus
        {
            get => getStaus();
        }
        public string getStaus()
        {
            if (this.Status == "Approved")
            {
                return "A";
            }
            else if (this.Status == "Failed" || this.Status == "Rejected")
            {
                return "R";
            }
            else
            {
                return "D";
            }
        }


    }
}
