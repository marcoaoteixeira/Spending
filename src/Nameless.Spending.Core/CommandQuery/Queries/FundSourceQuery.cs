using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nameless.Spending.Core.Models;
using Nameless.Spending.Core.Models.Filters;

namespace Nameless.Spending.Core.CommandQuery.Queries {
	public class FundSourceQuery : FilterQuery {
		#region Public Override Properties

		public override Type EntityType {
			get { return typeof(FundSource); }
		}

		#endregion

		#region Public Constructors

		public FundSourceQuery(FundSourceFilterModel model)
			: base(model) { }

		#endregion

		#region Protected Override Methods

		protected override void BuildFilterCriteria(ICollection<Expression<Func<IEntity, bool>>> specs, FilterModel model) {
			base.BuildFilterCriteria(specs, model);

			var fundSource = model as FundSourceFilterModel;

			if (!string.IsNullOrWhiteSpace(fundSource.Name)) {
				specs.Add(_ =>
					((FundSource)_).Name.Contains(fundSource.Name));
			}
		}

		#endregion
	}
}