#region

using System;

#endregion

namespace Roomies2.DAL.Model.Finance
{
    public class Transaction
    {
        public Transaction(int transactionId = default, string description = null, DateTime dateTime = default,
            int amount = default)
        {
            TransactionId = transactionId;
            Description = description ?? throw new ArgumentNullException(nameof(description));
            DateTime = dateTime;
            Amount = amount;
        }

        public int TransactionId { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public int Amount { get; set; }
    }
}