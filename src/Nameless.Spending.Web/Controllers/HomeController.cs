using System.Web.Http;

namespace Nameless.Spending.Web.Controllers {
	public class HomeController : WebApiControllerBase {
		#region Public Methods

		[HttpGet]
		public IHttpActionResult Index() {
			return Ok(T("Getting Started"));
		}

		#endregion
	}
}