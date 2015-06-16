using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nameless.Spending.Core.Models;
using Nameless.Spending.Core.Models.Filters;

namespace Nameless.Spending.Core.CommandQuery.Queries {
	public class BudgetItemQuery : FilterQuery {
		#region Public Override Properties

		public override Type EntityType {
			get { return typeof(BudgetItem); }
		}

		#endregion

		#region Public Constructors

		public BudgetItemQuery(BudgetItemFilterModel model)
			: base(model) { }

		#endregion

		#region Protected Override Methods

		protected override void BuildFilterCriteria(ICollection<Expression<Func<IEntity, bool>>> specs, FilterModel model) {
			base.BuildFilterCriteria(specs, model);

			var budgetItem = model as BudgetItemFilterModel;

			if (!string.IsNullOrWhiteSpace(budgetItem.Description)) {
				specs.Add(_ => ((BudgetItem)_).Description.Contains(budgetItem.Description));
			}

			// Between values (Value)
			if (budgetItem.MinValue != 0 && budgetItem.MaxValue != 0) {
				specs.Add(_ =>
				((BudgetItem)_).Value >= budgetItem.MinValue &&
				((BudgetItem)_).Value <= budgetItem.MaxValue);
			}

			// From MinValue to greater values.
			if (budgetItem.MinValue != 0 && budgetItem.MaxValue <= 0) {
				specs.Add(_ =>
				((BudgetItem)_).Value >= budgetItem.MinValue);
			}

			// From MaxValue to lower values.
			if (budgetItem.MinValue <= 0 && budgetItem.MaxValue != 0) {
				specs.Add(_ =>
				((BudgetItem)_).Value <= budgetItem.MaxValue);
			}

			if (budgetItem.BudgetID != 0) {
				specs.Add(_ => ((BudgetItem)_).Budget.ID == budgetItem.BudgetID);
			}
		}

		#endregion
	}
}