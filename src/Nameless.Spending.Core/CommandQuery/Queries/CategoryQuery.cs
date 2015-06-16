using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nameless.Spending.Core.Models;
using Nameless.Spending.Core.Models.Filters;

namespace Nameless.Spending.Core.CommandQuery.Queries {
	public class CategoryQuery : FilterQuery {
		#region Public Override Properties

		public override Type EntityType {
			get { return typeof(Category); }
		}

		#endregion

		#region Public Constructors

		public CategoryQuery(CategoryFilterModel model)
			: base(model) { }

		#endregion

		#region Protected Override Methods

		protected override void BuildFilterCriteria(ICollection<Expression<Func<IEntity, bool>>> specs, FilterModel model) {
			base.BuildFilterCriteria(specs, model);

			var category = model as CategoryFilterModel;

			if (!string.IsNullOrWhiteSpace(category.Description)) {
				specs.Add(_ => ((Category)_).Description.Contains(category.Description));
			}
		}

		#endregion
	}
}