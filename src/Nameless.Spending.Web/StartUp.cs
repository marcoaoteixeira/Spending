using System.Web.Http;
using Microsoft.Owin;
using Nameless.Framework.IoC;
using Nameless.Framework.IoC.Autofac;
using Nameless.Spending.Web;
using Owin;

[assembly: OwinStartup(typeof(StartUp))]

namespace Nameless.Spending.Web {
	public partial class StartUp {
		#region Private Read-Only Fields

		private readonly ICompositionRoot _compositionRoot;
		private readonly HttpConfiguration _configuration;

		#endregion

		#region Public Constructors

		public StartUp() {
			_compositionRoot = new CompositionRoot();
			_configuration = GlobalConfiguration.Configuration;
		}

		#endregion

		#region Public Methods

		public void Configuration(IAppBuilder app) {
			ConfigureFilters(_configuration);
			ConfigureRoutes(_configuration);
			ConfigureIoC(app, _compositionRoot, _configuration);
			ConfigureInfrastructure(_configuration);
			
			_configuration.EnsureInitialized();
		}

		#endregion
	}
}