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
	public class BudgetController : WebApiController {
		#region Private Read-Only Fields

		private readonly ICommandHandler<DeleteBudgetCommand> _deleteBudgetCommandHandler;
		private readonly ICommandHandler<StoreBudgetCommand> _storeBudgetCommandHandler;
		private readonly IQueryHandler<BudgetQuery, Page<BudgetViewModel>> _budgetQueryHandler;

		private readonly ICommandHandler<DeleteBudgetItemCommand> _deleteBudgetItemCommandHandler;
		private readonly ICommandHandler<StoreBudgetItemCommand> _storeBudgetItemCommandHandler;
		private readonly IQueryHandler<BudgetItemQuery, Page<BudgetItemViewModel>> _budgetItemQueryHandler;

		#endregion

		#region Public Constructors

		public BudgetController(IMapper mapper
			, ICommandHandler<DeleteBudgetCommand> deleteBudgetCommandHandler
			, ICommandHandler<StoreBudgetCommand> storeBudgetCommandHandler
			, IQueryHandler<BudgetQuery, Page<BudgetViewModel>> budgetQueryHandler

			, ICommandHandler<DeleteBudgetItemCommand> deleteBudgetItemCommandHandler
			, ICommandHandler<StoreBudgetItemCommand> storeBudgetItemCommandHandler
			, IQueryHandler<BudgetItemQuery, Page<BudgetItemViewModel>> budgetItemQueryHandler)
			: base(mapper) {
			Guard.Against.Null(deleteBudgetCommandHandler, "deleteBudgetCommandHandler");
			Guard.Against.Null(storeBudgetCommandHandler, "storeBudgetCommandHandler");
			Guard.Against.Null(budgetQueryHandler, "budgetQueryHandler");

			Guard.Against.Null(deleteBudgetItemCommandHandler, "deleteBudgetItemCommandHandler");
			Guard.Against.Null(storeBudgetItemCommandHandler, "storeBudgetItemCommandHandler");
			Guard.Against.Null(budgetItemQueryHandler, "budgetItemQueryHandler");

			_deleteBudgetCommandHandler = deleteBudgetCommandHandler;
			_storeBudgetCommandHandler = storeBudgetCommandHandler;
			_budgetQueryHandler = budgetQueryHandler;

			_deleteBudgetItemCommandHandler = deleteBudgetItemCommandHandler;
			_storeBudgetItemCommandHandler = storeBudgetItemCommandHandler;
			_budgetItemQueryHandler = budgetItemQueryHandler;
		}

		#endregion

		#region Public Methods

		[HttpDelete]
		[Route("api/budget/{id:long:min(1)}")]
		public IHttpActionResult Delete(long id) {
			if (id <= 0) {
				return BadRequest("Invalid ID");
			}

			var command = new DeleteBudgetCommand { ID = id };

			_deleteBudgetCommandHandler.Handle(command);

			return Ok(Request.CreateResponse(HttpStatusCode.NoContent));
		}

		[HttpDelete]
		[Route("api/budget/{budget:long:min(1)}/{item:long:min(1)}")]
		public IHttpActionResult DeleteItem(long budget, long item) {
			if (budget <= 0) {
				return BadRequest("Invalid ID for budget.");
			}

			if (item <= 0) {
				return BadRequest("Invalid ID for budget item.");
			}

			var command = new DeleteBudgetItemCommand {
				BudgetID = budget,
				ItemID = item
			};

			_deleteBudgetItemCommandHandler.Handle(command);

			return Ok(Request.CreateResponse(HttpStatusCode.NoContent));
		}

		[HttpGet]
		[Route("api/budget/{id:long:min(1)}", Name = "Budget")]
		public IHttpActionResult Get(long id) {
			if (id <= 0) {
				return BadRequest("Invalid ID");
			}

			var model = new BudgetFilterModel { ID = id };
			var query = new BudgetQuery(model);
			var result = _budgetQueryHandler.Handle(query);

			return Ok(result.Items.SingleOrDefault());
		}

		[HttpGet]
		[Route("api/budget")]
		public IHttpActionResult Get([FromUri]BudgetFilterModel model) {
			var query = new BudgetQuery(model);
			var result = _budgetQueryHandler.Handle(query);

			return Ok(result);
		}

		[HttpGet]
		[Route("api/budget/{budget:long:min(1)}/{item:long:min(1)}", Name = "BudgetItem")]
		public IHttpActionResult GetItem(long budget, long item) {
			if (budget <= 0) {
				return BadRequest("Invalid ID for budget.");
			}

			if (item <= 0) {
				return BadRequest("Invalid ID for budget item.");
			}

			var model = new BudgetItemFilterModel {
				BudgetID = budget,
				ID = item
			};
			var query = new BudgetItemQuery(model);
			var items = _budgetItemQueryHandler.Handle(query).Items;

			return Ok(items.SingleOrDefault());
		}

		[HttpGet]
		[Route("api/budget/{budget:long:min(1)}/items/{pageNumber:int=1}/{pageSize:int=10}")]
		public IHttpActionResult GetItems(int budget, int pageNumber, int pageSize) {
			if (budget <= 0) {
				return BadRequest("Invalid ID for budget.");
			}

			var model = new BudgetItemFilterModel {
				BudgetID = budget,
				PageNumber = pageNumber,
				PageSize = pageSize
			};
			var query = new BudgetItemQuery(model);
			var result = _budgetItemQueryHandler.Handle(query);

			return Ok(result);
		}

		[HttpPost]
		[Route("api/budget")]
		public IHttpActionResult Post(BudgetBindingModel model) {
			model.ID = 0;

			var command = Mapper.Map<BudgetBindingModel, StoreBudgetCommand>(model);

			_storeBudgetCommandHandler.Handle(command);

			model.ID = command.ID;

			return Created(Url.Route("Budget", new { id = model.ID }), model);
		}

		[HttpPost]
		[Route("api/budget/{budget:long:min(1)}")]
		public IHttpActionResult PostItem(long budget, BudgetItemBindingModel model) {
			model.ID = 0;

			var command = Mapper.Map<BudgetItemBindingModel, StoreBudgetItemCommand>(model);

			_storeBudgetItemCommandHandler.Handle(command);

			model.ID = command.ID;

			return Created(Url.Route("BudgetItem", new { budget = model.BudgetID, item = model.ID }), model);
		}

		[HttpPut]
		[Route("api/budget/{id:long:min(1)}")]
		public IHttpActionResult Put(long id, BudgetBindingModel model) {
			if (id <= 0) {
				return BadRequest("Invalid ID");
			}

			model.ID = id;

			var command = Mapper.Map<BudgetBindingModel, StoreBudgetCommand>(model);

			_storeBudgetCommandHandler.Handle(command);

			return Ok(model);
		}

		#endregion
	}
}