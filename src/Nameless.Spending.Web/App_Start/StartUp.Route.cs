using System.Web.Http;

namespace Nameless.Spending.Web {
	public partial class StartUp {
		#region Public Methods

		public void ConfigureRoutes(HttpConfiguration configuration) {
			// Web API configuration and services

			// Web API routes
			configuration.MapHttpAttributeRoutes();

			/* ***** Default Routes **************************************** */
			configuration.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
			/* **************************************** Default Routes ***** */
		}

		#endregion
	}
}