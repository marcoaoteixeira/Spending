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
		[Route("api/fundSource/{fundSource:long:min(1)}/credit/{credit:long:min(1)}")]
		public IHttpActionResult DeleteCredit(long fundSource, long credit) {
			var command = new DeleteCreditCommand {
				FundSourceID = fundSource,
				CreditID = credit
			};

			_deleteCreditCommandHandler.Handle(command);

			return Ok(Request.CreateResponse(HttpStatusCode.NoContent));
		}

		[HttpGet]
		[Route("api/fundSource/{fundSource:long:min(1)}/credit/{credit:long:min(1)}/", Name = "GetCredit")]
		public IHttpActionResult GetCredit(long fundSource, long credit) {
			var view = QueryCreditByID(fundSource, credit);

			if (view == null) {
				return NotFound();
			}

			return Ok(view);
		}

		[HttpGet]
		[Route("api/fundSource/{fundSource:long:min(1)}/credits")]
		public IHttpActionResult GetCredit(long fundSource, [FromUri]CreditFilterModel filter) {
			filter.FundSourceID = fundSource;

			var query = new CreditQuery(filter);
			var result = _creditQueryHandler.Handle(query);

			return Ok(result);
		}

		[HttpPost]
		[Route("api/fundSource/{fundSource:long:min(1)}/credit")]
		public IHttpActionResult PostCredit(long fundSource, [FromBody]CreditBindingModel binding) {
			var command = Mapper.Map<CreditBindingModel, CreateCreditCommand>(binding);

			command.FundSourceID = fundSource;

			_createCreditCommandHandler.Handle(command);

			var view = QueryCreditByID(fundSource, command.CreditID);

			return Created(Url.Route("GetCredit", new { id = view.ID }), view);
		}

		[HttpPut]
		[Route("api/fundSource/{fundSource:long:min(1)}/credit/{credit:long:min(1)}")]
		public IHttpActionResult PutCredit(long fundSource, long credit, CreditBindingModel binding) {
			var command = Mapper.Map<CreditBindingModel, AlterCreditCommand>(binding);

			command.CreditID = credit;
			command.CurrentFundSourceID = fundSource;

			_alterCreditCommandHandler.Handle(command);

			var view = QueryCreditByID(fundSource, command.CreditID);

			return Ok(view);
		}

		#endregion

		#region Operation Debit

		[HttpDelete]
		[Route("api/fundSource/{fundSource:long:min(1)}/debit/{debit:long:min(1)}")]
		public IHttpActionResult DeleteDebit(long fundSource, long debit) {
			var command = new DeleteDebitCommand {
				FundSourceID = fundSource,
				DebitID = debit
			};

			_deleteDebitCommandHandler.Handle(command);

			return Ok(Request.CreateResponse(HttpStatusCode.NoContent));
		}

		[HttpGet]
		[Route("api/fundSource/{fundSource:long:min(1)}/debit/{debit:long:min(1)}/", Name = "GetDebit")]
		public IHttpActionResult GetDebit(long fundSource, long debit) {
			var view = QueryDebitByID(fundSource, debit);

			if (view == null) {
				return NotFound();
			}

			return Ok(view);
		}

		[HttpGet]
		[Route("api/fundSource/{fundSource:long:min(1)}/debits")]
		public IHttpActionResult GetDebit(long fundSource, [FromUri]DebitFilterModel filter) {
			filter.FundSourceID = fundSource;

			var query = new DebitQuery(filter);
			var result = _debitQueryHandler.Handle(query);

			return Ok(result);
		}

		[HttpPost]
		[Route("api/fundSource/{fundSource:long:min(1)}/debit")]
		public IHttpActionResult PostDebit(long fundSource, [FromBody]DebitBindingModel binding) {
			var command = Mapper.Map<DebitBindingModel, CreateDebitCommand>(binding);

			command.FundSourceID = fundSource;

			_createDebitCommandHandler.Handle(command);

			var view = QueryDebitByID(fundSource, command.DebitID);

			return Created(Url.Route("GetDebit", new { id = view.ID }), view);
		}

		[HttpPut]
		[Route("api/fundSource/{fundSource:long:min(1)}/debit/{debit:long:min(1)}")]
		public IHttpActionResult PutDebit(long fundSource, long debit, DebitBindingModel binding) {
			var command = Mapper.Map<DebitBindingModel, AlterDebitCommand>(binding);

			command.DebitID = debit;
			command.CurrentFundSourceID = fundSource;

			_alterDebitCommandHandler.Handle(command);

			var view = QueryDebitByID(fundSource, command.DebitID);

			return Ok(view);
		}

		#endregion

		#endregion

		#region Private Methods

		private CreditViewModel QueryCreditByID(long fundSource, long credit) {
			var filter = new CreditFilterModel {
				FundSourceID = fundSource,
				ID = credit
			};
			var query = new CreditQuery(filter);
			var result = _creditQueryHandler.Handle(query);

			return result.Items.SingleOrDefault();
		}

		private DebitViewModel QueryDebitByID(long fundSource, long debit) {
			var filter = new DebitFilterModel {
				FundSourceID = fundSource,
				ID = debit
			};
			var query = new DebitQuery(filter);
			var result = _debitQueryHandler.Handle(query);

			return result.Items.SingleOrDefault();
		}

		#endregion
	}
}