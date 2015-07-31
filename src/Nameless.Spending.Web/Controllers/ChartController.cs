using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Nameless.Spending.Core.Models.Filters;

namespace Nameless.Spending.Web.Controllers {
	public class ChartController : WebApiControllerBase {
		#region Private Read-Only Fields



		#endregion

		#region Public Constructors



		#endregion

		#region Public Methods

		[HttpGet]
		[Route("api/charts")]
		public IHttpActionResult GetBalanceForPeriod([FromUri]BalancePerPeriodFilter filter) {
			throw new NotImplementedException();	
		}

		#endregion
	}
}