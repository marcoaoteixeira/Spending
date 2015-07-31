using System.Web.Http;
using Nameless.Framework;
using Nameless.Framework.CommandQuery;
using Nameless.Spending.Core.CommandQuery.Queries;
using Nameless.Spending.Core.Models.Views;

namespace Nameless.Spending.Web.Controllers {
	public class BalanceController : WebApiControllerBase {
		#region Private Read-Only Fields

		private readonly IQueryHandler<GetBalanceQuery, BalanceViewModel> _queryHandler;

		#endregion

		#region Public Constructors

		public BalanceController(IQueryHandler<GetBalanceQuery, BalanceViewModel> queryHandler) {
			Guard.Against.Null(queryHandler, "queryHandler");

			_queryHandler = queryHandler;
		}

		#endregion

		#region Public Methods

		[HttpGet]
		[Route("api/balance")]
		public IHttpActionResult Get() {
			var result = _queryHandler.Handle(new GetBalanceQuery());

			return Ok(result);
		}

		#endregion
	}
}