using System;
using System.Collections.Generic;
using System.Linq;
using Nameless.Framework;
using Nameless.Framework.CommandQuery;
using Nameless.Framework.Data;
using Nameless.Framework.Data.NHibernate;
using Nameless.Spending.Core.Models;
using Nameless.Spending.Core.Models.Views;

namespace Nameless.Spending.Core.CommandQuery.Queries {
	public class DebitPerCategoryQuery : IQuery {
		#region Public Properties

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		#endregion
	}

	public class DebitPerCategoryQueryHandler : IQueryHandler<DebitPerCategoryQuery, IEnumerable<DebitPerCategoryViewModel>> {
		#region Private Read-Only Fields

		private readonly IRepository _repository;

		#endregion

		#region Public Constructors

		public DebitPerCategoryQueryHandler(IRepository repository) {
			Guard.Against.Null(repository, "repository");

			_repository = repository;
		}

		#endregion

		#region IQueryHandler<DebitPerCategoryQuery,IEnumerable<DebitPerCategoryViewModel>> Members

		public IEnumerable<DebitPerCategoryViewModel> Handle(DebitPerCategoryQuery query) {
			if (query.StartDate > query.EndDate) {
				throw new InvalidOperationException("Start date should be lower than end date.");
			}

			var debits = _repository.Query<Debit>()
				.Where(_ => _.Date.Date >= query.StartDate.Date && _.Date.Date <= query.EndDate.Date)
				.ToList();
			var groups = debits.GroupBy(_ => _.Category.GetBreadCrumb()).OrderBy(_ => _.Key);

			return groups.Select(group => {
				return new DebitPerCategoryViewModel {
					
				};
			});
		}

		#endregion
	}
}
