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
	public sealed class FilterQueryHandler<TQuery, TOutput> : IQueryHandler<TQuery, Page<TOutput>>
		where TQuery : IQuery
		where TOutput : EntityViewModel {
		#region Private Read-Only Fields

		private readonly IRepository _repository;
		private readonly IMapper _mapper;

		#endregion

		#region Public Constructors

		public FilterQueryHandler(IRepository repository, IMapper mapper) {
			Guard.Against.Null(repository, "repository");
			Guard.Against.Null(mapper, "mapper");

			_repository = repository;
			_mapper = mapper;
		}

		#endregion

		#region Public Override Methods

		public Page<TOutput> Handle(TQuery query) {
			var innerQuery = query as FilterQuery;

			var method = _repository.GetType()
				.GetMethod("Query")
				.MakeGenericMethod(innerQuery.EntityType);
			var queryable = innerQuery.ExecuteFilter((method.Invoke(_repository, null) as IQueryable<IEntity>));
			var totalItemCount = queryable.Count();
			var enumerableOfEntity = typeof(IEnumerable<>).MakeGenericType(innerQuery.EntityType);
			var items = _mapper.Map(queryable.ToList(), enumerableOfEntity, typeof(IEnumerable<TOutput>)) as IEnumerable<TOutput>;

			return new Page<TOutput>(items) {
				PageNumber = innerQuery.PageNumber,
				PageSize = innerQuery.PageSize,
				TotalItemCount = totalItemCount
			};
		}

		#endregion
	}
}