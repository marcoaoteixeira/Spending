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

		private readonly ICommandHandler<DeleteCategoryCommand> _deleteCategoryCommandHandler;
		private readonly ICommandHandler<StoreCategoryCommand> _storeCategoryCommandHandler;
		private readonly IQueryHandler<CategoryQuery, Page<CategoryViewModel>> _categoryQueryHandler;

		#endregion

		#region Public Constructors

		public CategoryController(IMapper mapper
			, ICommandHandler<DeleteCategoryCommand> deleteCategoryCommandHandler
			, ICommandHandler<StoreCategoryCommand> storeCategoryCommandHandler
			, IQueryHandler<CategoryQuery, Page<CategoryViewModel>> categoryQueryHandler)
			: base(mapper) {
			Guard.Against.Null(deleteCategoryCommandHandler, "deleteCategoryCommandHandler");
			Guard.Against.Null(storeCategoryCommandHandler, "storeCategoryCommandHandler");
			Guard.Against.Null(categoryQueryHandler, "categoryQueryHandler");

			_deleteCategoryCommandHandler = deleteCategoryCommandHandler;
			_storeCategoryCommandHandler = storeCategoryCommandHandler;
			_categoryQueryHandler = categoryQueryHandler;
		}

		#endregion

		#region Public Methods

		[HttpDelete]
		[Route("api/category/{id:long:min(1)}")]
		public IHttpActionResult Delete(long id) {
			if (id <= 0) {
				return BadRequest("Invalid ID");
			}

			var command = new DeleteCategoryCommand { ID = id };

			_deleteCategoryCommandHandler.Handle(command);

			return Ok(Request.CreateResponse(HttpStatusCode.NoContent));
		}

		[HttpGet]
		[Route("api/category/{id:long:min(1)}", Name = "Category")]
		public IHttpActionResult Get(long id) {
			if (id <= 0) {
				return BadRequest("Invalid ID");
			}

			var model = new CategoryFilterModel { ID = id };
			var query = new CategoryQuery(model);
			var result = _categoryQueryHandler.Handle(query);

			return Ok(result.Items.SingleOrDefault());
		}

		[HttpGet]
		[Route("api/categories")]
		public IHttpActionResult Get([FromUri]CategoryFilterModel model) {
			var query = new CategoryQuery(model);
			var result = _categoryQueryHandler.Handle(query);

			return Ok(result);
		}

		[HttpPost]
		[Route("api/category")]
		public IHttpActionResult Post(CategoryBindingModel model) {
			model.ID = 0;

			var command = Mapper.Map<CategoryBindingModel, StoreCategoryCommand>(model);

			_storeCategoryCommandHandler.Handle(command);

			model.ID = command.ID;

			return Created(Url.Route("Category", new { id = model.ID }), model);
		}

		[HttpPut]
		[Route("api/category/{id:long:min(1)}")]
		public IHttpActionResult Put(long id, CategoryBindingModel model) {
			if (id <= 0) {
				return BadRequest("Invalid ID");
			}

			model.ID = id;

			var command = Mapper.Map<CategoryBindingModel, StoreCategoryCommand>(model);

			_storeCategoryCommandHandler.Handle(command);

			return Ok(model);
		}

		#endregion
	}
}