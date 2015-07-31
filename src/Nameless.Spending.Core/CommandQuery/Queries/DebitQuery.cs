using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nameless.Spending.Core.Models;
using Nameless.Spending.Core.Models.Filters;

namespace Nameless.Spending.Core.CommandQuery.Queries {
	public class DebitQuery : FilterQuery {
		#region Public Override Properties

		public override Type EntityType {
			get { return typeof(Debit); }
		}

		#endregion

		#region Public Constructors

		public DebitQuery(DebitFilterModel model)
			: base(model) { }

		#endregion

		#region Protected Override Methods

		protected override void BuildFilterCriteria(ICollection<Expression<Func<IEntity, bool>>> specs, FilterModel model) {
			base.BuildFilterCriteria(specs, model);

			var debit = model as DebitFilterModel;

			if (!string.IsNullOrWhiteSpace(debit.Description)) {
				specs.Add(_ => ((Debit)_).Description.Contains(debit.Description));
			}

			// Between values (Value)
			if (debit.MinValue != 0 && debit.MaxValue != 0) {
				specs.Add(_ =>
					((Debit)_).Value >= debit.MinValue &&
					((Debit)_).Value <= debit.MaxValue);
			}

			// From MinValue to greater values.
			if (debit.MinValue != 0 && debit.MaxValue <= 0) {
				specs.Add(_ =>
					((Debit)_).Value >= debit.MinValue);
			}

			// From MaxValue to lower values.
			if (debit.MinValue <= 0 && debit.MaxValue != 0) {
				specs.Add(_ =>
					((Debit)_).Value <= debit.MaxValue);
			}

			// Between dates (Date)
			if (debit.MinDate != DateTime.MinValue && debit.MaxDate != DateTime.MinValue) {
				specs.Add(_ =>
					((Debit)_).Date.Date >= debit.MinDate.Date &&
					((Debit)_).Date.Date <= debit.MaxDate.Date);
			}

			// From MinDate to greater dates.
			if (debit.MinDate != DateTime.MinValue && debit.MaxDate == DateTime.MinValue) {
				specs.Add(_ =>
					((Debit)_).Date.Date >= debit.MinDate.Date);
			}

			// From MaxDate to lower dates.
			if (debit.MinDate == DateTime.MinValue && debit.MaxDate != DateTime.MinValue) {
				specs.Add(_ =>
					((Debit)_).Date.Date <= debit.MaxDate.Date);
			}

			if (debit.CategoryID != 0) {
				specs.Add(_ =>
					((Debit)_).Category.ID == debit.CategoryID);
			}
		}

		#endregion
	}
}