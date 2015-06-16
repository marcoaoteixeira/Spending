using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nameless.Spending.Core.Models;
using Nameless.Spending.Core.Models.Filters;

namespace Nameless.Spending.Core.CommandQuery.Queries {
	public class CreditQuery : FilterQuery {
		#region Public Override Properties

		public override Type EntityType {
			get { return typeof(Credit); }
		}

		#endregion

		#region Public Constructors

		public CreditQuery(CreditFilterModel model)
			: base(model) { }

		#endregion

		#region Protected Override Methods

		protected override void BuildFilterCriteria(ICollection<Expression<Func<IEntity, bool>>> specs, FilterModel model) {
			base.BuildFilterCriteria(specs, model);

			var credit = model as CreditFilterModel;

			if (!string.IsNullOrWhiteSpace(credit.Description)) {
				specs.Add(_ => ((Credit)_).Description.Contains(credit.Description));
			}

			// Between values (Value)
			if (credit.MinValue != 0 && credit.MaxValue != 0) {
				specs.Add(_ =>
				((Credit)_).Value >= credit.MinValue &&
				((Credit)_).Value <= credit.MaxValue);
			}

			// From MinValue to greater values.
			if (credit.MinValue != 0 && credit.MaxValue <= 0) {
				specs.Add(_ =>
				((Credit)_).Value >= credit.MinValue);
			}

			// From MaxValue to lower values.
			if (credit.MinValue <= 0 && credit.MaxValue != 0) {
				specs.Add(_ =>
				((Credit)_).Value <= credit.MaxValue);
			}

			// Between dates (Date)
			if (credit.MinDate != DateTime.MinValue && credit.MaxDate != DateTime.MinValue) {
				specs.Add(_ =>
					((Credit)_).Date.Date >= credit.MinDate.Date &&
					((Credit)_).Date.Date <= credit.MaxDate.Date);
			}

			// From MinDate to greater dates.
			if (credit.MinDate != DateTime.MinValue && credit.MaxDate == DateTime.MinValue) {
				specs.Add(_ =>
					((Credit)_).Date.Date >= credit.MinDate.Date);
			}

			// From MaxDate to lower dates.
			if (credit.MinDate == DateTime.MinValue && credit.MaxDate != DateTime.MinValue) {
				specs.Add(_ =>
					((Credit)_).Date.Date <= credit.MaxDate.Date);
			}

			if (credit.FundSourceID != 0) {
				specs.Add(_ => ((Credit)_).FundSource.ID == credit.FundSourceID);
			}
		}

		#endregion
	}
}