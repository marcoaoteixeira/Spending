using System;
using System.Linq;
using Nameless.Framework.CommandQuery;
using Nameless.Spending.Core.Models;
using Nameless.Spending.Core.Models.Filters;

namespace Nameless.Spending.Core.CommandQuery.Queries {
	public abstract class FilterQuery : IPagedQuery {
		#region Private Read-Only Fields

		private readonly FilterModel _data;
		private readonly Filter _filter;

		#endregion

		#region Public Abstract Properties

		public abstract Type EntityType { get; }
		public abstract Type ViewModelType { get; }

		#endregion

		#region Public Properties

		public FilterModel Data {
			get { return _data; }
		}

		#endregion

		#region Protected Properties

		protected Filter Filter {
			get { return _filter; }
		}

		#endregion

		#region Protected Constructors

		protected FilterQuery(Filter filter, FilterModel data) {
			_filter = filter;
			_data = data;
		}

		#endregion

		#region Public Virtual Methods

		public virtual IQueryable<IEntity> ExecuteFilter(IQueryable<IEntity> source) {
			return _filter.Evaluate(source, _data);
		}

		#endregion

		#region IPagedQuery Members

		public int PageNumber {
			get { throw new NotImplementedException(); }
		}

		public int PageSize {
			get { throw new NotImplementedException(); }
		}

		#endregion
	}
}