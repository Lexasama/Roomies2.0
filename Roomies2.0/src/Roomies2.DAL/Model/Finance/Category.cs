#region

using System;
using System.Collections.Generic;

#endregion

namespace Roomies2.DAL.Model.Finance
{
    public class Category
    {
        public Category(int categoryId = default, string categoryName = null, string categoryPicture = null,
            List<Budget> budgets = null)
        {
            CategoryId = categoryId;
            CategoryName = categoryName ?? throw new ArgumentNullException(nameof(categoryName));
            CategoryPicture = categoryPicture ?? throw new ArgumentNullException(nameof(categoryPicture));
            Budgets = budgets ?? throw new ArgumentNullException(nameof(budgets));
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryPicture { get; set; }
        public List<Budget> Budgets { get; set; }
    }
}