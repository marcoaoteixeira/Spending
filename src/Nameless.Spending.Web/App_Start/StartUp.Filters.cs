using System.Web.Http;
using Nameless.Framework.Infrastructure;
using Nameless.Framework.WebApi.Filters;

namespace Nameless.Spending.Web {
	public partial class StartUp {
		#region Public Methods

		public void ConfigureFilters(HttpConfiguration config) {
			config.Filters.Add(new ValidateModelStateAttribute());
			config.Filters.Add(new StopwatchActionFilter());
		}

		#endregion
	}
}