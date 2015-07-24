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
	public class FundSourceController : WebApiController {
		#region Private Read-Only Fields

		private readonly ICommandHandler<AlterFundSourceCommand> _alterFundSourceCommandHandler;
		private readonly ICommandHandler<CreateFundSourceCommand> _createFundSourceCommandHandler;
		private readonly ICommandHandler<DeleteFundSourceCommand> _deleteFundSourceCommandHandler;
		private readonly IQueryHandler<FundSourceQuery, Page<FundSourceViewModel>> _fundSourceQueryHandler;

		#endregion

		#region Public Constructors

		public FundSourceController(IMapper mapper
			, ICommandHandler<AlterFundSourceCommand> alterFundSourceCommandHandler
			, ICommandHandler<CreateFundSourceCommand> createFundSourceCommandHandler
			, ICommandHandler<DeleteFundSourceCommand> deleteFundSourceCommandHandler
			, IQueryHandler<FundSourceQuery, Page<FundSourceViewModel>> fundSourceQueryHandler)
			: base(mapper) {
			Guard.Against.Null(alterFundSourceCommandHandler, "alterFundSourceCommandHandler");
			Guard.Against.Null(createFundSourceCommandHandler, "createFundSourceCommandHandler");
			Guard.Against.Null(deleteFundSourceCommandHandler, "deleteFundSourceCommandHandler");
			Guard.Against.Null(fundSourceQueryHandler, "fundSourceQueryHandler");

			_alterFundSourceCommandHandler = alterFundSourceCommandHandler;
			_createFundSourceCommandHandler = createFundSourceCommandHandler;
			_deleteFundSourceCommandHandler = deleteFundSourceCommandHandler;
			_fundSourceQueryHandler = fundSourceQueryHandler;
		}

		#endregion

		#region Public Methods

		[HttpDelete]
		[Route("api/fundSource/{fundSource:long:min(1)}")]
		public IHttpActionResult Delete(long fundSource) {
			var command = new DeleteFundSourceCommand {
				ID = fundSource
			};

			_deleteFundSourceCommandHandler.Handle(command);

			return Ok(Request.CreateResponse(HttpStatusCode.NoContent));
		}

		[HttpGet]
		[Route("api/fundSource/{fundSource:long:min(1)}", Name = "GetFundSource")]
		public IHttpActionResult Get(long fundSource) {
			var view = QueryByID(fundSource);

			if (view == null) {
				return NotFound();
			}

			return Ok(view);
		}

		[HttpGet]
		[Route("api/fundSources")]
		public IHttpActionResult Get([FromUri]FundSourceFilterModel filter) {
			var query = new FundSourceQuery(filter);
			var result = _fundSourceQueryHandler.Handle(query);

			return Ok(result);
		}

		[HttpPost]
		[Route("api/fundSource")]
		public IHttpActionResult Post([FromBody]FundSourceBindingModel binding) {
			var command = Mapper.Map<FundSourceBindingModel, CreateFundSourceCommand>(binding);

			_createFundSourceCommandHandler.Handle(command);

			var view = QueryByID(command.FundSourceID);

			return Created(Url.Route("FundSource", new { fundSource = view.ID }), view);
		}

		[HttpPut]
		[Route("api/fundSource/{fundSource:long:min(1)}")]
		public IHttpActionResult Put(long fundSource, [FromBody]FundSourceBindingModel binding) {
			var command = Mapper.Map<FundSourceBindingModel, AlterFundSourceCommand>(binding);

			command.FundSourceID = fundSource;

			_alterFundSourceCommandHandler.Handle(command);

			var view = QueryByID(fundSource);

			return Ok(view);
		}

		#endregion

		#region Private Methods

		private FundSourceViewModel QueryByID(long fundSource) {
			var filter = new FundSourceFilterModel { ID = fundSource };
			var query = new FundSourceQuery(filter);
			var result = _fundSourceQueryHandler.Handle(query);

			return result.Items.SingleOrDefault();
		}

		#endregion
	}
}