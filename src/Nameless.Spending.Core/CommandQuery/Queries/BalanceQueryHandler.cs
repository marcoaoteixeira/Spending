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
	public class BalanceQuery : IQuery { }

	public class BalanceQueryHandler : IQueryHandler<BalanceQuery, BalanceViewModel> {
		#region Private Static Read-Only Fields

		private static readonly string SQL = "SELECT COALESCE((SELECT SUM(credits.value) FROM credits), 0.000) AS TotalCredits, COALESCE((SELECT SUM(debits.value) FROM debits), 0.000) AS TotalDebits";

		#endregion

		#region Private Read-Only Fields

		private readonly IRepository _repository;

		#endregion

		#region Public Constructors

		public BalanceQueryHandler(IRepository repository) {
			Guard.Against.Null(repository, "repository");

			_repository = repository;
		}

		#endregion

		#region IQueryHandler<BalanceQuery,BalanceViewModel> Members

		public BalanceViewModel Handle(BalanceQuery query) {
			var result = _repository.ExecuteDirective(new SQLDirective {
				Text = SQL
			});

			return new BalanceViewModel {
				TotalCredits = Convert.ToDecimal(result[0].TotalCredits),
				TotalDebits = Convert.ToDecimal(result[0].TotalDebits)
			};
		}

		#endregion
	}

	public class BalancePerPeriodQuery : IQuery {
		#region Public Properties

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		#endregion
	}

	public class BalancePerPeriodQueryHandler : IQueryHandler<BalancePerPeriodQuery, IEnumerable<BalancePerPeriodViewModel>> {
		#region Private Read-Only Fields

		private readonly IRepository _repository;

		#endregion

		#region Public Constructors

		public BalancePerPeriodQueryHandler(IRepository repository) {
			Guard.Against.Null(repository, "repository");

			_repository = repository;
		}

		#endregion

		#region IQueryHandler<BalancePerPeriodQuery,IEnumerable<BalancePerPeriodViewModel>> Members

		public IEnumerable<BalancePerPeriodViewModel> Handle(BalancePerPeriodQuery query) {
			if (query.StartDate > query.EndDate) {
				throw new InvalidOperationException("Start date should be lower than end date.");
			}

			var credits = _repository.Query<Credit>().Where(_ => _.Date.Date >= query.StartDate.Date && _.Date.Date <= query.EndDate.Date).ToList();
			var debits = _repository.Query<Debit>().Where(_ => _.Date.Date >= query.StartDate.Date && _.Date.Date <= query.EndDate.Date).ToList();
			var totalDays = Convert.ToInt64((query.EndDate.Date - query.StartDate.Date).TotalDays + 1);
			var result = new List<BalancePerPeriodViewModel>();

			totalDays.Times(day => {
				var period = query.StartDate.Date.AddDays(day);
				var balance = new BalancePerPeriodViewModel {
					TotalCredits = credits.Where(_ => _.Date.Date == period).Sum(_ => _.Value),
					TotalDebits = debits.Where(_ => _.Date.Date == period).Sum(_ => _.Value),
					Period = period
				};

				result.Add(balance);
			});

			return result;
		}

		#endregion
	}
}