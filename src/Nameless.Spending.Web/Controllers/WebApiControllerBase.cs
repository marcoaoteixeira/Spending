using System.Web.Http;
using Nameless.Framework.Localization;
using Nameless.Framework.Logging;

namespace Nameless.Spending.Web.Controllers {
	public abstract class WebApiControllerBase : ApiController {
		#region Private Fields

		private ILogger _logger;
		private Localizer _localizer;

		#endregion

		#region Public Properties

		public ILogger Log {
			get { return _logger ?? NullLogger.Instance; }
			set { _logger = value ?? NullLogger.Instance; }
		}

		public Localizer T {
			get { return _localizer ?? NullLocalizer.Instance; }
			set { _localizer = value ?? NullLocalizer.Instance; }
		}

		#endregion
	}
}