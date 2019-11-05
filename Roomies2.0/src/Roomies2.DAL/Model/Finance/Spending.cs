#region

using System;

#endregion

namespace Roomies2.DAL.Model.Finance
{
    public class Spending
    {
        public Spending(int spendingId = default, DateTime dateTime = default, int amount = default,
            string description = null)
        {
            SpendingId = spendingId;
            DateTime = dateTime;
            Amount = amount;
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public int SpendingId { get; set; }
        public DateTime DateTime { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
    }
}