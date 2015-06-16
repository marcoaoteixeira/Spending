using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nameless.Framework;
using Nameless.Framework.CommandQuery;
using Nameless.Spending.Core.Models;
using Nameless.Spending.Core.Models.Filters;

namespace Nameless.Spending.Core.CommandQuery.Queries {
	public abstract class FilterQuery : IQuery {
		#region Private Read-Only Fields

		private readonly ICollection<Expression<Func<IEntity, bool>>> _specs = new HashSet<Expression<Func<IEntity, bool>>>();
		private readonly FilterModel _model;

		#endregion

		#region Public Properties

		public int PageNumber {
			get { return _model.PageNumber; }
		}

		public int PageSize {
			get { return _model.PageSize; }
		}

		#endregion

		#region Public Abstract Properties

		public abstract Type EntityType { get; }

		#endregion

		#region Protected Constructors

		protected FilterQuery(FilterModel model) {
			Guard.Against.Null(model, "model");

			_model = model;
		}

		#endregion

		#region Public Methods

		public IQueryable<IEntity> ExecuteFilter(IQueryable<IEntity> source) {
			var specs = new HashSet<Expression<Func<IEntity, bool>>>();

			BuildFilterCriteria(specs, _model);

			return specs.Aggregate(source, (current, spec) => current.Where(spec));
		}

		#endregion

		#region Protected Virtual Methods

		protected virtual void BuildFilterCriteria(ICollection<Expression<Func<IEntity, bool>>> specs, FilterModel model) {
			if (model.ID != 0) {
				specs.Add(entity => entity.ID == model.ID);

				return;
			}

			// Between dates (DateCreated)
			if (model.MinDateCreated != DateTime.MinValue && model.MaxDateCreated != DateTime.MinValue) {
				specs.Add(entity =>
					entity.DateCreated.Date >= model.MinDateCreated.Date &&
					entity.DateCreated.Date <= model.MaxDateCreated.Date);
			}

			// From MinDateCreate to greater dates.
			if (model.MinDateCreated != DateTime.MinValue && model.MaxDateCreated == DateTime.MinValue) {
				specs.Add(entity =>
					entity.DateCreated.Date >= model.MinDateCreated.Date);
			}

			// From MaxDateCreate to lower dates.
			if (model.MinDateCreated == DateTime.MinValue && model.MaxDateCreated != DateTime.MinValue) {
				specs.Add(entity =>
					entity.DateCreated.Date <= model.MaxDateCreated.Date);
			}

			// Between dates (DateModified)
			if (model.MinDateModified != DateTime.MinValue && model.MaxDateModified != DateTime.MinValue) {
				specs.Add(entity =>
					entity.DateModified.Date >= model.MinDateModified.Date &&
					entity.DateModified.Date <= model.MaxDateModified.Date);
			}

			// From MinDateCreate to greater dates.
			if (model.MinDateModified != DateTime.MinValue && model.MaxDateModified == DateTime.MinValue) {
				specs.Add(entity =>
					entity.DateModified.Date >= model.MinDateModified.Date);
			}

			// From MaxDateCreate to lower dates.
			if (model.MinDateModified == DateTime.MinValue && model.MaxDateModified != DateTime.MinValue) {
				specs.Add(entity =>
					entity.DateModified.Date <= model.MaxDateModified.Date);
			}
		}

		#endregion
	}
}