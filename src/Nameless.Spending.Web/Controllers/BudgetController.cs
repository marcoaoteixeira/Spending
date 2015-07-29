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


		private readonly ICommandHandler<AlterBudgetCommand> _alterBudgetCommandHandler;
		private readonly ICommandHandler<CreateBudgetCommand> _createBudgetCommandHandler;
		private readonly ICommandHandler<DeleteBudgetCommand> _deleteBudgetCommandHandler;

		private readonly IQueryHandler<BudgetQuery, Page<BudgetViewModel>> _budgetQueryHandler;

		private readonly ICommandHandler<CreateBudgetItemCommand> _createBudgetItemCommandHandler;
		private readonly ICommandHandler<DeleteBudgetItemCommand> _deleteBudgetItemCommandHandler;

		private readonly IQueryHandler<BudgetItemQuery, Page<BudgetItemViewModel>> _budgetItemQueryHandler;

		#endregion

		#region Public Constructors

		public BudgetController(IMapper mapper
			, ICommandHandler<AlterBudgetCommand> alterBudgetCommandHandler
			, ICommandHandler<CreateBudgetCommand> createBudgetCommandHandler
			, ICommandHandler<DeleteBudgetCommand> deleteBudgetCommandHandler
			, IQueryHandler<BudgetQuery, Page<BudgetViewModel>> budgetQueryHandler

			, ICommandHandler<CreateBudgetItemCommand> createBudgetItemCommandHandler
			, ICommandHandler<DeleteBudgetItemCommand> deleteBudgetItemCommandHandler
			, IQueryHandler<BudgetItemQuery, Page<BudgetItemViewModel>> budgetItemQueryHandler)
			: base(mapper) {
			Guard.Against.Null(alterBudgetCommandHandler, "alterBudgetCommandHandler");
			Guard.Against.Null(createBudgetCommandHandler, "createBudgetCommandHandler");
			Guard.Against.Null(deleteBudgetCommandHandler, "deleteBudgetCommandHandler");
			Guard.Against.Null(budgetQueryHandler, "budgetQueryHandler");

			Guard.Against.Null(createBudgetItemCommandHandler, "alterBudgetItemCommandHandler");
			Guard.Against.Null(deleteBudgetItemCommandHandler, "deleteBudgetItemCommandHandler");
			Guard.Against.Null(budgetItemQueryHandler, "budgetItemQueryHandler");

			_alterBudgetCommandHandler = alterBudgetCommandHandler;
			_createBudgetCommandHandler = createBudgetCommandHandler;
			_deleteBudgetCommandHandler = deleteBudgetCommandHandler;
			_budgetQueryHandler = budgetQueryHandler;

			_createBudgetItemCommandHandler = createBudgetItemCommandHandler;
			_deleteBudgetItemCommandHandler = deleteBudgetItemCommandHandler;
			_budgetItemQueryHandler = budgetItemQueryHandler;
		}

		#endregion

		#region Public Methods

		[HttpDelete]
		[Route("api/budget/{budget:long:min(1)}")]
		public IHttpActionResult Delete(long budget) {
			var command = new DeleteBudgetCommand {
				BudgetID = budget
			};

			_deleteBudgetCommandHandler.Handle(command);

			return Ok(Request.CreateResponse(HttpStatusCode.NoContent));
		}

		[HttpGet]
		[Route("api/budget/{budget:long:min(1)}", Name = "GetBudget")]
		public IHttpActionResult Get(long budget) {
			var view = QueryBudgetByID(budget);

			if (view == null) {
				return NotFound();
			}

			return Ok(view);
		}

		[HttpGet]
		[Route("api/budgets")]
		public IHttpActionResult Get([FromUri]BudgetFilterModel filter) {
			var query = new BudgetQuery(filter);
			var result = _budgetQueryHandler.Handle(query);

			return Ok(result);
		}

		[HttpPost]
		[Route("api/budget")]
		public IHttpActionResult Post([FromBody]BudgetBindingModel binding) {
			var command = Mapper.Map<BudgetBindingModel, CreateBudgetCommand>(binding);

			_createBudgetCommandHandler.Handle(command);

			var view = QueryBudgetByID(command.BudgetID);

			return Created(Url.Route("GetBudget", new { budget = view.ID }), view);
		}

		[HttpPut]
		[Route("api/budget/{budget:long:min(1)}")]
		public IHttpActionResult Put(long budget, [FromBody]BudgetBindingModel binding) {
			var command = Mapper.Map<BudgetBindingModel, AlterBudgetCommand>(binding);

			command.BudgetID = budget;

			_alterBudgetCommandHandler.Handle(command);

			var view = QueryBudgetByID(budget);

			return Ok(view);
		}

		[HttpDelete]
		[Route("api/budget/{budget:long:min(1)}/item/{item:long:min(1)}")]
		public IHttpActionResult DeleteItem(long budget, long item) {
			var command = new DeleteBudgetItemCommand {
				BudgetID = budget,
				ItemID = item
			};

			_deleteBudgetItemCommandHandler.Handle(command);

			return Ok(Request.CreateResponse(HttpStatusCode.NoContent));
		}

		[HttpGet]
		[Route("api/budget/{budget:long:min(1)}/item/{item:long:min(1)}", Name = "GetBudgetItem")]
		public IHttpActionResult GetItem(long budget, long item) {
			var view = QueryBudgetItemByID(budget, item);

			if (view == null) {
				return NotFound();
			}

			return Ok(view);
		}

		[HttpGet]
		[Route("api/budget/{budget:long:min(1)}/items")]
		public IHttpActionResult GetItems([FromUri]BudgetItemFilterModel filter) {
			var query = new BudgetItemQuery(filter);
			var result = _budgetItemQueryHandler.Handle(query);

			return Ok(result);
		}

		[HttpPost]
		[Route("api/budget/{budget:long:min(1)}/item")]
		public IHttpActionResult PostItem(long budget, [FromBody]BudgetItemBindingModel binding) {
			var command = Mapper.Map<BudgetItemBindingModel, CreateBudgetItemCommand>(binding);

			command.BudgetID = budget;

			_createBudgetItemCommandHandler.Handle(command);

			var view = QueryBudgetItemByID(budget, command.BudgetItemID);

			return Created(Url.Route("GetBudgetItem", new { budget = view.BudgetID, item = view.ID }), view);
		}

		#endregion

		#region Private Methods

		private BudgetViewModel QueryBudgetByID(long budget) {
			var filter = new BudgetFilterModel { ID = budget };
			var query = new BudgetQuery(filter);
			var result = _budgetQueryHandler.Handle(query);

			return result.Items.SingleOrDefault();
		}

		private BudgetItemViewModel QueryBudgetItemByID(long budget, long budgetItem) {
			var filter = new BudgetItemFilterModel {
				BudgetID = budget,
				ID = budgetItem
			};
			var query = new BudgetItemQuery(filter);
			var result = _budgetItemQueryHandler.Handle(query);

			return result.Items.SingleOrDefault();
		}

		#endregion
	}
}