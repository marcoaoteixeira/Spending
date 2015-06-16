using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nameless.Spending.Core.Models;
using Nameless.Spending.Core.Models.Filters;
using Nameless.Spending.Core.Models.Views;

namespace Nameless.Spending.Core.CommandQuery.Queries {
	public class BudgetQuery : FilterQuery {
		#region Public Override Properties

		public override Type EntityType {
			get { return typeof(Budget); }
		}

		#endregion

		#region Public Constructors

		public BudgetQuery(BudgetFilterModel model)
			: base(model) { }

		#endregion

		#region Protected Override Methods

		protected override void BuildFilterCriteria(ICollection<Expression<Func<IEntity, bool>>> specs, FilterModel model) {
			base.BuildFilterCriteria(specs, model);

			var budget = model as BudgetFilterModel;

			if (!string.IsNullOrWhiteSpace(budget.Description)) {
				specs.Add(_ => ((Budget)_).Description.Contains(budget.Description));
			}

			// Between dates (PeriodMonth & PeriodYear)
			if ((budget.MinPeriodMonth != 0 && budget.MinPeriodYear != 0) &&
				(budget.MaxPeriodMonth != 0 && budget.MaxPeriodYear != 0)) {
				specs.Add(_ =>
					((Budget)_).Period >= budget.GetMinPeriod() &&
					((Budget)_).Period <= budget.GetMaxPeriod());
			}

			// From PeriodMonth & PeriodYear to greater dates.
			if ((budget.MinPeriodMonth != 0 && budget.MinPeriodYear != 0) &&
				(budget.MaxPeriodMonth == 0 && budget.MaxPeriodYear == 0)) {
				specs.Add(_ =>
					((Budget)_).Period >= budget.GetMinPeriod());
			}

			// From PeriodMonth & PeriodYear to lower dates.
			if ((budget.MinPeriodMonth == 0 && budget.MinPeriodYear == 0) &&
				(budget.MaxPeriodMonth != 0 && budget.MaxPeriodYear != 0)) {
				specs.Add(_ =>
					((Budget)_).Period <= budget.GetMaxPeriod());
			}

			// Between values (Total)
			if (budget.MinTotal != 0 && budget.MaxTotal != 0) {
				specs.Add(_ =>
					((Budget)_).Total >= budget.MinTotal &&
					((Budget)_).Total <= budget.MaxTotal);
			}

			// From MinTotal to greater values.
			if (budget.MinTotal != 0 && budget.MaxTotal <= 0) {
				specs.Add(_ =>
					((Budget)_).Total >= budget.MinTotal);
			}

			// From MaxTotal to lower values.
			if (budget.MinTotal <= 0 && budget.MaxTotal != 0) {
				specs.Add(_ =>
					((Budget)_).Total <= budget.MaxTotal);
			}

			if (budget.CategoryID != 0) {
				specs.Add(_ => ((Budget)_).Category.ID == budget.CategoryID);
			}
		}

		#endregion
	}
}