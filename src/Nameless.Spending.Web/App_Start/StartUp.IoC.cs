using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Nameless.Framework;
using Nameless.Framework.CommandQuery;
using Nameless.Framework.Configuration;
using Nameless.Framework.Infrastructure;
using Nameless.Framework.IoC;
using Nameless.Framework.IoC.Autofac;
using Nameless.Framework.Localization.Xml;
using Nameless.Framework.Logging.Log4net;
using Nameless.Framework.ObjectModel;
using Nameless.Spending.Core.CommandQuery.Queries;
using Nameless.Spending.Core.Data.NHibernate;
using NHibernate.Mapping.ByCode;
using Owin;

namespace Nameless.Spending.Web {
	public partial class StartUp {
		#region Private Read-Only Fields

		private readonly Assembly CoreAssembly = Assembly.Load("Nameless.Spending.Core");
		private readonly Assembly WebAssembly = Assembly.Load("Nameless.Spending.Web");
		private readonly Assembly FrameworkAssembly = Assembly.Load("Nameless.Framework");
		private readonly Assembly FrameworkExtrasAssembly = Assembly.Load("Nameless.Framework.Extras");
		private readonly Assembly FrameworkWebAssembly = Assembly.Load("Nameless.Framework.Web");
		private readonly Assembly FrameworkWebExtrasAssembly = Assembly.Load("Nameless.Framework.Web.Extras");

		#endregion

		#region Public Methods

		public void ConfigureIoC(IAppBuilder app, ICompositionRoot compositionRoot, HttpConfiguration configuration) {
			compositionRoot.Register(register => {
				var builder = (ContainerBuilder)register;

				builder.RegisterModule(new AutoMapperMapperModule(new[] { CoreAssembly }));
				builder.RegisterModule(new CachingModule { UseVirtualPathMonitorProvider = true });
				builder.RegisterModule(new CommandQueryModule(new[] { CoreAssembly }));
				builder.RegisterModule(new ConfigurationModule(typeof(XmlConfigurationProvider)));
				builder.RegisterModule<FrameworkModule>();
				builder.RegisterModule(new LocalizationModule(typeof(XmlLocalizationParser)));
				builder.RegisterModule(new LoggingModule(typeof(LoggerFactory)));
				builder.RegisterModule(new NHibernateModule(new[] { CoreAssembly, FrameworkAssembly, FrameworkExtrasAssembly, FrameworkWebAssembly, FrameworkWebExtrasAssembly }));
				builder.RegisterModule<XmlSchemaValidatorModule>();
				builder.RegisterModule<ApplicationModule>();

				builder.RegisterApiControllers(new[] { WebAssembly });
			});

			compositionRoot.Resolve(resolve => {
				var container = (IContainer)resolve;

				configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
				app.UseAutofacMiddleware(container);
			});

			app.UseAutofacWebApi(configuration);
			app.UseWebApi(configuration);
		}

		#endregion

		#region Private Inner Classes

		private class ApplicationModule : AutofacModule {
			#region Public Override Methods

			protected override void Load(ContainerBuilder builder) {
				builder.RegisterType<ModelInspector>().As<IModelInspector>().SingleInstance();
				builder.RegisterType<HostingEnvironmentWrapper>().As<IHostingEnvironment>().SingleInstance();

				builder
					.RegisterGeneric(typeof(FilterQueryHandler<,>))
					.As(typeof(IQueryHandler<,>))
					.InstancePerDependency();
			}

			#endregion
		}

		#endregion
	}
}