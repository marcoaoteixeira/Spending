using System.Collections.Generic;
using System.Web.Http;
using Nameless.Framework;
using Nameless.Framework.CommandQuery;
using Nameless.Spending.Core.CommandQuery.Queries;
using Nameless.Spending.Core.Models.Filters;
using Nameless.Spending.Core.Models.Views;

namespace Nameless.Spending.Web.Controllers {
	public class DataController : WebApiControllerBase {
		#region Private Read-Only Fields

		private readonly IQueryHandler<BalanceQuery, BalanceViewModel> _balanceQueryHandler;
		private readonly IQueryHandler<BalancePerPeriodQuery, IEnumerable<BalancePerPeriodViewModel>> _balancePerPeriodQueryHandler;

		#endregion

		#region Public Constructors

		public DataController(IQueryHandler<BalanceQuery, BalanceViewModel> balanceQueryHandler
			, IQueryHandler<BalancePerPeriodQuery, IEnumerable<BalancePerPeriodViewModel>> balancePerPeriodQueryHandler) {
			Guard.Against.Null(balanceQueryHandler, "balanceQueryHandler");
			Guard.Against.Null(balancePerPeriodQueryHandler, "balancePerPeriodQueryHandler");

			_balanceQueryHandler = balanceQueryHandler;
			_balancePerPeriodQueryHandler = balancePerPeriodQueryHandler;
		}

		#endregion

		#region Public Methods

		[HttpGet]
		[Route("api/data/balance")]
		public IHttpActionResult GetBalance() {
			var result = _balanceQueryHandler.Handle(new BalanceQuery());

			return Ok(result);
		}

		[HttpGet]
		[Route("api/data/balance/period/{startDate:DateTime}/{endDate:DateTime}")]
		public IHttpActionResult GetBalanceForPeriod([FromUri]BalancePerPeriodFilter filter) {
			var result = _balancePerPeriodQueryHandler.Handle(new BalancePerPeriodQuery {
				EndDate = filter.EndDate,
				StartDate = filter.StartDate
			});

			return Ok(result);
		}

		#endregion
	}
}