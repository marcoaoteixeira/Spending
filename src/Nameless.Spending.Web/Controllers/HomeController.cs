using System.Web.Http;
using Nameless.Framework.CommandQuery;
using Nameless.Framework.ObjectModel;
using Nameless.Spending.Core.CommandQuery.Queries;
using Nameless.Spending.Core.Models.Filters;
using Nameless.Spending.Core.Models.Views;
using System.Linq;
using Nameless.Framework.Data;
using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Web.Controllers {
	public class HomeController : WebApiControllerBase {
		#region Private Read-Only Fields

		private readonly IQueryHandler<CategoryQuery, Page<CategoryViewModel>> _queryHandler;
		private readonly IRepository _repository;

		#endregion

		public HomeController(IQueryHandler<CategoryQuery, Page<CategoryViewModel>> queryHandler, IRepository repository) {
			_repository = repository;
			_queryHandler = queryHandler;
		}

		#region Public Methods

		[HttpGet]
		public IHttpActionResult Index() {
			var result = _repository.FindOne<Category>(_ => _.ID == 4);

			if (result != null) {
				result.GetBreadCrumb();
				result.GetBreadCrumb();
				result.GetBreadCrumb();

				result.Ancestors.Add(new Category {

				});

				result.GetBreadCrumb();
			}
			

			return Ok(result);
		}

		#endregion
	}
}