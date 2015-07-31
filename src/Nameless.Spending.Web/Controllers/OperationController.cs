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
	public class OperationController : WebApiController {
		#region Private Read-Only Fields

		private readonly ICommandHandler<AlterCreditCommand> _alterCreditCommandHandler;
		private readonly ICommandHandler<CreateCreditCommand> _createCreditCommandHandler;
		private readonly ICommandHandler<DeleteCreditCommand> _deleteCreditCommandHandler;

		private readonly IQueryHandler<CreditQuery, Page<CreditViewModel>> _creditQueryHandler;

		private readonly ICommandHandler<AlterDebitCommand> _alterDebitCommandHandler;
		private readonly ICommandHandler<CreateDebitCommand> _createDebitCommandHandler;
		private readonly ICommandHandler<DeleteDebitCommand> _deleteDebitCommandHandler;

		private readonly IQueryHandler<DebitQuery, Page<DebitViewModel>> _debitQueryHandler;

		#endregion

		#region Public Controllers

		public OperationController(IMapper mapper
			, ICommandHandler<AlterCreditCommand> alterCreditCommandHandler
			, ICommandHandler<CreateCreditCommand> createCreditCommandHandler
			, ICommandHandler<DeleteCreditCommand> deleteCreditCommandHandler
			, IQueryHandler<CreditQuery, Page<CreditViewModel>> creditQueryHandler

			, ICommandHandler<AlterDebitCommand> alterDebitCommandHandler
			, ICommandHandler<CreateDebitCommand> createDebitCommandHandler
			, ICommandHandler<DeleteDebitCommand> deleteDebitCommandHandler
			, IQueryHandler<DebitQuery, Page<DebitViewModel>> debitQueryHandler)
			: base(mapper) {
			Guard.Against.Null(alterCreditCommandHandler, "alterCreditCommandHandler");
			Guard.Against.Null(createCreditCommandHandler, "createCreditCommandHandler");
			Guard.Against.Null(deleteCreditCommandHandler, "deleteCreditCommandHandler");
			Guard.Against.Null(creditQueryHandler, "creditQueryHandler");

			Guard.Against.Null(alterDebitCommandHandler, "alterDebitCommandHandler");
			Guard.Against.Null(createDebitCommandHandler, "createDebitCommandHandler");
			Guard.Against.Null(deleteDebitCommandHandler, "deleteDebitCommandHandler");
			Guard.Against.Null(debitQueryHandler, "debitQueryHandler");

			_alterCreditCommandHandler = alterCreditCommandHandler;
			_createCreditCommandHandler = createCreditCommandHandler;
			_deleteCreditCommandHandler = deleteCreditCommandHandler;
			_creditQueryHandler = creditQueryHandler;

			_alterDebitCommandHandler = alterDebitCommandHandler;
			_createDebitCommandHandler = createDebitCommandHandler;
			_deleteDebitCommandHandler = deleteDebitCommandHandler;
			_debitQueryHandler = debitQueryHandler;
		}

		#endregion

		#region Public Methods

		#region Operation Credit

		[HttpDelete]
		[Route("api/operation/credit/{credit:long:min(1)}")]
		public IHttpActionResult DeleteCredit(long credit) {
			var command = new DeleteCreditCommand {
				CreditID = credit
			};

			_deleteCreditCommandHandler.Handle(command);

			return Ok(Request.CreateResponse(HttpStatusCode.NoContent));
		}

		[HttpGet]
		[Route("api/operation/credit/{credit:long:min(1)}/", Name = "GetCredit")]
		public IHttpActionResult GetCredit(long credit) {
			var view = QueryCreditByID(credit);

			if (view == null) {
				return NotFound();
			}

			return Ok(view);
		}

		[HttpGet]
		[Route("api/operation/credits")]
		public IHttpActionResult GetCredit([FromUri]CreditFilterModel filter) {
			var query = new CreditQuery(filter);
			var result = _creditQueryHandler.Handle(query);

			return Ok(result);
		}

		[HttpPost]
		[Route("api/operation/credit")]
		public IHttpActionResult PostCredit([FromBody]CreditBindingModel binding) {
			var command = Mapper.Map<CreditBindingModel, CreateCreditCommand>(binding);

			_createCreditCommandHandler.Handle(command);

			var view = QueryCreditByID(command.CreditID);

			return Created(Url.Route("GetCredit", new { id = view.ID }), view);
		}

		[HttpPut]
		[Route("api/operation/credit/{credit:long:min(1)}")]
		public IHttpActionResult PutCredit(long credit, CreditBindingModel binding) {
			var command = Mapper.Map<CreditBindingModel, AlterCreditCommand>(binding);

			command.CreditID = credit;

			_alterCreditCommandHandler.Handle(command);

			var view = QueryCreditByID(command.CreditID);

			return Ok(view);
		}

		#endregion

		#region Operation Debit

		[HttpDelete]
		[Route("api/operation/debit/{debit:long:min(1)}")]
		public IHttpActionResult DeleteDebit(long debit) {
			var command = new DeleteDebitCommand {
				DebitID = debit
			};

			_deleteDebitCommandHandler.Handle(command);

			return Ok(Request.CreateResponse(HttpStatusCode.NoContent));
		}

		[HttpGet]
		[Route("api/operation/debit/{debit:long:min(1)}/", Name = "GetDebit")]
		public IHttpActionResult GetDebit(long debit) {
			var view = QueryDebitByID(debit);

			if (view == null) {
				return NotFound();
			}

			return Ok(view);
		}

		[HttpGet]
		[Route("api/operation/debits")]
		public IHttpActionResult GetDebit([FromUri]DebitFilterModel filter) {
			var query = new DebitQuery(filter);
			var result = _debitQueryHandler.Handle(query);

			return Ok(result);
		}

		[HttpPost]
		[Route("api/operation/debit")]
		public IHttpActionResult PostDebit([FromBody]DebitBindingModel binding) {
			var command = Mapper.Map<DebitBindingModel, CreateDebitCommand>(binding);

			_createDebitCommandHandler.Handle(command);

			var view = QueryDebitByID(command.DebitID);

			return Created(Url.Route("GetDebit", new { id = view.ID }), view);
		}

		[HttpPut]
		[Route("api/operation/debit/{debit:long:min(1)}")]
		public IHttpActionResult PutDebit(long debit, DebitBindingModel binding) {
			var command = Mapper.Map<DebitBindingModel, AlterDebitCommand>(binding);

			command.DebitID = debit;
			
			_alterDebitCommandHandler.Handle(command);

			var view = QueryDebitByID(command.DebitID);

			return Ok(view);
		}

		#endregion

		#endregion

		#region Private Methods

		private CreditViewModel QueryCreditByID(long credit) {
			var filter = new CreditFilterModel {
				ID = credit
			};
			var query = new CreditQuery(filter);
			var result = _creditQueryHandler.Handle(query);

			return result.Items.SingleOrDefault();
		}

		private DebitViewModel QueryDebitByID(long debit) {
			var filter = new DebitFilterModel {
				ID = debit
			};
			var query = new DebitQuery(filter);
			var result = _debitQueryHandler.Handle(query);

			return result.Items.SingleOrDefault();
		}

		#endregion
	}
}