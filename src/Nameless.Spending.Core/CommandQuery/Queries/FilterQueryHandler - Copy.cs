using System.Collections.Generic;
using System.Linq;
using Nameless.Framework;
using Nameless.Framework.CommandQuery;
using Nameless.Framework.Data;
using Nameless.Framework.Mapper;
using Nameless.Framework.ObjectModel;
using Nameless.Spending.Core.Models;
using Nameless.Spending.Core.Models.Views;

namespace Nameless.Spending.Core.CommandQuery.Queries {
	public sealed class FilterQueryHandler<TFilterQuery, TOutput> : PagedQueryHandlerBase<TFilterQuery, TOutput>
		where TFilterQuery : FilterQuery
		where TOutput : EntityViewModel {
		#region Public Constructors

		public FilterQueryHandler(IRepository repository, IMapper mapper)
			: base(repository, mapper) { }

		#endregion

		#region IQueryHandler<EntityQueryBase,Page<EntityViewModel>> Members

		public Page<EntityViewModel> Handle(FilterQuery query) {
			var method = Repository.GetType()
				.GetMethod("Query")
				.MakeGenericMethod(query.EntityType);
			var queryable = method.Invoke(Repository, null) as IQueryable<IEntity>;

			// Apply "WHERE" clause
			queryable = query.ExecuteFilter(queryable);

			var totalItemCount = queryable.Count();
			var paged = !query.Data.DisablePaginate
				? queryable.Skip((query.Data.PageNumber - 1) * query.Data.PageSize).Take(query.Data.PageSize)
				: queryable;
			var enumerableOfEntity = typeof(IEnumerable<>).MakeGenericType(query.EntityType);
			var enumerableOfEntityViewModel = typeof(IEnumerable<>).MakeGenericType(query.ViewModelType);
			var items = Mapper.Map(paged.ToList(), enumerableOfEntity, enumerableOfEntityViewModel) as IEnumerable<EntityViewModel>;

			return new Page<EntityViewModel>(items) {
				PageNumber = query.Data.PageNumber,
				PageSize = query.Data.PageSize,
				TotalItemCount = totalItemCount
			};
		}

		#endregion

		#region Public Override Methods

		public override Page<TOutput> Handle(TFilterQuery pagedQuery) {
			throw new System.NotImplementedException();
		}

		#endregion
	}
}