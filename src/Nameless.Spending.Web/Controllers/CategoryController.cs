using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nameless.Framework;
using Nameless.Framework.CommandQuery;
using Nameless.Framework.Mapper;
using Nameless.Framework.ObjectModel;
using Nameless.Spending.Core.CommandQuery.Commands;
using Nameless.Spending.Core.CommandQuery.Queries;
using Nameless.Spending.Core.Models.Bindings;
using Nameless.Spending.Core.Models.Filters;
using Nameless.Spending.Core.Models.Views;

namespace Nameless.Spending.Web.Controllers {
	public class CategoryController : WebApiController {
		#region Private Read-Only Fields

		private readonly ICommandHandler<AlterCategoryCommand> _alterCategoryCommandHandler;
		private readonly ICommandHandler<CreateCategoryCommand> _createCategoryCommandHandler;
		private readonly ICommandHandler<DeleteCategoryCommand> _deleteCategoryCommandHandler;

		private readonly IQueryHandler<CategoryQuery, Page<CategoryViewModel>> _categoryQueryHandler;

		#endregion

		#region Public Constructors

		public CategoryController(IMapper mapper
			, ICommandHandler<AlterCategoryCommand> alterCategoryCommandHandler
			, ICommandHandler<CreateCategoryCommand> createCategoryCommandHandler
			, ICommandHandler<DeleteCategoryCommand> deleteCategoryCommandHandler
			, IQueryHandler<CategoryQuery, Page<CategoryViewModel>> categoryQueryHandler)
			: base(mapper) {
			Guard.Against.Null(alterCategoryCommandHandler, "alterCategoryCommandHandler");
			Guard.Against.Null(createCategoryCommandHandler, "createCategoryCommandHandler");
			Guard.Against.Null(deleteCategoryCommandHandler, "deleteCategoryCommandHandler");
			Guard.Against.Null(categoryQueryHandler, "categoryQueryHandler");

			_alterCategoryCommandHandler = alterCategoryCommandHandler;
			_createCategoryCommandHandler = createCategoryCommandHandler;
			_deleteCategoryCommandHandler = deleteCategoryCommandHandler;
			_categoryQueryHandler = categoryQueryHandler;
		}

		#endregion

		#region Public Methods

		[HttpDelete]
		[Route("api/category/{category:long:min(1)}")]
		public IHttpActionResult Delete(long category) {
			var command = new DeleteCategoryCommand {
				CategoryID = category
			};

			_deleteCategoryCommandHandler.Handle(command);

			return Ok(Request.CreateResponse(HttpStatusCode.NoContent));
		}

		[HttpGet]
		[Route("api/category/{category:long:min(1)}", Name = "GetCategory")]
		public IHttpActionResult Get(long category) {
			var view = QueryByID(category);

			if (view == null) {
				return NotFound();
			}

			return Ok(view);
		}

		[HttpGet]
		[Route("api/categories")]
		public IHttpActionResult Get([FromUri]CategoryFilterModel filter) {
			var query = new CategoryQuery(filter);
			var result = _categoryQueryHandler.Handle(query);

			return Ok(result);
		}

		[HttpPost]
		[Route("api/category")]
		public IHttpActionResult Post([FromBody]CategoryBindingModel binding) {
			var command = Mapper.Map<CategoryBindingModel, CreateCategoryCommand>(binding);

			_createCategoryCommandHandler.Handle(command);

			var view = QueryByID(command.CategoryID);

			return Created(Url.Route("GetCategory", new { category = view.ID }), view);
		}

		[HttpPut]
		[Route("api/category/{category:long:min(1)}")]
		public IHttpActionResult Put(long category, [FromBody]CategoryBindingModel binding) {
			var command = Mapper.Map<CategoryBindingModel, AlterCategoryCommand>(binding);

			command.CategoryID = category;

			_alterCategoryCommandHandler.Handle(command);

			var view = QueryByID(category);

			return Ok(view);
		}

		#endregion

		#region Private Methods

		private CategoryViewModel QueryByID(long category) {
			var filter = new CategoryFilterModel { ID = category };
			var query = new CategoryQuery(filter);
			var result = _categoryQueryHandler.Handle(query);

			return result.Items.SingleOrDefault();
		}

		#endregion
	}
}