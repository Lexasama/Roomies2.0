#region

using System;
using System.Collections.Generic;

#endregion

namespace Roomies2.DAL.Model.Finance
{
    public class Budget
    {
        public Budget(int budgetId = default, int amount = default, DateTime beginDate = default,
            DateTime endDate = default, List<Spending> spendings = null)
        {
            BudgetId = budgetId;
            Amount = amount;
            BeginDate = beginDate;
            EndDate = endDate;
            Spendings = spendings ?? throw new ArgumentNullException(nameof(spendings));
        }

        public int BudgetId { get; set; }
        public int Amount { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Spending> Spendings { get; set; }
    }
}