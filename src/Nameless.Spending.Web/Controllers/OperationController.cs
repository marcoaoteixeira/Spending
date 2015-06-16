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
	[RoutePrefix("api/operation")]
	public class OperationController : WebApiController {
		#region Private Read-Only Fields

		private readonly ICommandHandler<DeleteCreditCommand> _deleteCreditCommandHandler;
		private readonly ICommandHandler<StoreCreditCommand> _storeCreditCommandHandler;
		private readonly IQueryHandler<CreditQuery, Page<CreditViewModel>> _creditQueryHandler;

		private readonly ICommandHandler<DeleteDebitCommand> _deleteDebitCommandHandler;
		private readonly ICommandHandler<StoreDebitCommand> _storeDebitCommandHandler;
		private readonly IQueryHandler<DebitQuery, Page<DebitViewModel>> _debitQueryHandler;

		#endregion

		#region Public Controllers

		public OperationController(IMapper mapper
			, ICommandHandler<DeleteCreditCommand> deleteCreditCommandHandler
			, ICommandHandler<StoreCreditCommand> storeCreditCommandHandler
			, IQueryHandler<CreditQuery, Page<CreditViewModel>> creditQueryHandler

			, ICommandHandler<DeleteDebitCommand> deleteDebitCommandHandler
			, ICommandHandler<StoreDebitCommand> storeDebitCommandHandler
			, IQueryHandler<DebitQuery, Page<DebitViewModel>> debitQueryHandler)
			: base(mapper) {
			Guard.Against.Null(deleteCreditCommandHandler, "deleteCreditCommandHandler");
			Guard.Against.Null(storeCreditCommandHandler, "storeCreditCommandHandler");
			Guard.Against.Null(creditQueryHandler, "creditQueryHandler");

			Guard.Against.Null(deleteDebitCommandHandler, "deleteDebitCommandHandler");
			Guard.Against.Null(storeDebitCommandHandler, "storeDebitCommandHandler");
			Guard.Against.Null(debitQueryHandler, "debitQueryHandler");

			_deleteCreditCommandHandler = deleteCreditCommandHandler;
			_storeCreditCommandHandler = storeCreditCommandHandler;
			_creditQueryHandler = creditQueryHandler;

			_deleteDebitCommandHandler = deleteDebitCommandHandler;
			_storeDebitCommandHandler = storeDebitCommandHandler;
			_debitQueryHandler = debitQueryHandler;
		}

		#endregion

		#region Public Methods

		#region Operation Credit

		[HttpDelete]
		[Route("credit/{id:long:min(1)}")]
		public IHttpActionResult DeleteCredit(long id) {
			if (id <= 0) {
				return BadRequest("Invalid ID");
			}

			var command = new DeleteCreditCommand { ID = id };

			_deleteCreditCommandHandler.Handle(command);

			return Ok(Request.CreateResponse(HttpStatusCode.NoContent));
		}

		[HttpGet]
		[Route("credit/{id:long:min(1)}", Name = "Credit")]
		public IHttpActionResult GetCredit(long id) {
			if (id <= 0) {
				return BadRequest("Invalid ID for credit.");
			}

			var model = new CreditFilterModel { ID = id };
			var query = new CreditQuery(model);
			var result = _creditQueryHandler.Handle(query);

			return Ok(result.Items.SingleOrDefault());
		}

		[HttpGet]
		[Route("credits")]
		public IHttpActionResult GetCredit([FromUri]CreditFilterModel model) {
			var query = new CreditQuery(model);
			var result = _creditQueryHandler.Handle(query);

			return Ok(result);
		}

		[HttpPost]
		[Route("credit")]
		public IHttpActionResult PostCredit([FromBody]CreditBindingModel model) {
			model.ID = 0;

			var command = Mapper.Map<CreditBindingModel, StoreCreditCommand>(model);

			_storeCreditCommandHandler.Handle(command);

			model.ID = command.ID;

			return Created(Url.Route("Credit", new { id = model.ID }), model);
		}

		[HttpPut]
		[Route("credit/{id:long:min(1)}")]
		public IHttpActionResult PutCredit(long id, CreditBindingModel model) {
			if (id <= 0) {
				return BadRequest("Invalid ID for credit.");
			}

			model.ID = id;

			var command = Mapper.Map<CreditBindingModel, StoreCreditCommand>(model);

			_storeCreditCommandHandler.Handle(command);

			return Ok(model);
		}

		#endregion

		#region Operation Debit

		[HttpDelete]
		[Route("debit/{id:long:min(1)}")]
		public IHttpActionResult DeleteDebit(long id) {
			if (id <= 0) {
				return BadRequest("Invalid ID for debit.");
			}

			var command = new DeleteDebitCommand { ID = id };

			_deleteDebitCommandHandler.Handle(command);

			return Ok(Request.CreateResponse(HttpStatusCode.NoContent));
		}

		[HttpGet]
		[Route("debit/{id:long:min(1)}", Name = "Debit")]
		public IHttpActionResult GetDebit(long id) {
			if (id <= 0) {
				return BadRequest("Invalid ID for debit.");
			}

			var model = new DebitFilterModel { ID = id };
			var query = new DebitQuery(model);
			var result = _debitQueryHandler.Handle(query);

			return Ok(result.Items.SingleOrDefault());
		}

		[HttpGet]
		[Route("debits")]
		public IHttpActionResult GetDebit([FromUri]DebitFilterModel model) {
			var query = new DebitQuery(model);
			var result = _debitQueryHandler.Handle(query);

			return Ok(result);
		}

		[HttpPost]
		[Route("debit")]
		public IHttpActionResult PostDebit([FromBody]DebitBindingModel model) {
			model.ID = 0;

			var command = Mapper.Map<DebitBindingModel, StoreDebitCommand>(model);

			_storeDebitCommandHandler.Handle(command);

			model.ID = command.ID;

			return Created(Url.Route("Operation", new {
				type = "debit",
				id = model.ID
			}), model);
		}

		[HttpPut]
		[Route("debit/{id:long:min(1)}")]
		public IHttpActionResult PutDebit(long id, DebitBindingModel model) {
			if (id <= 0) {
				return BadRequest("Invalid ID");
			}

			model.ID = id;

			var command = Mapper.Map<DebitBindingModel, StoreDebitCommand>(model);

			_storeDebitCommandHandler.Handle(command);

			return Ok(model);
		}

		#endregion

		#endregion
	}
}