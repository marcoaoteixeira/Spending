using System.Web.Http;
using Nameless.Framework.Infrastructure;

namespace Nameless.Spending.Web {
	public partial class StartUp {
		#region Public Methods

		public void ConfigureFilters(HttpConfiguration config) {
			config.Filters.Add(new ValidateModelStateAttribute());
		}

		#endregion
	}
}