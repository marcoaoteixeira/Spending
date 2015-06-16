using Nameless.Framework;
using Nameless.Framework.Mapper;

namespace Nameless.Spending.Web.Controllers {
	public abstract class WebApiController : WebApiControllerBase {
		#region Private Read-Only Fields

		private readonly IMapper _mapper;

		#endregion

		#region Protected Properties

		protected IMapper Mapper {
			get { return _mapper; }
		}

		#endregion

		#region Protected Constructors

		protected WebApiController(IMapper mapper) {
			Guard.Against.Null(mapper, "mapper");

			_mapper = mapper;
		}

		#endregion
	}
}