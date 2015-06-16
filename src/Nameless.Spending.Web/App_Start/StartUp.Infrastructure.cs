using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Nameless.Spending.Web {
	public partial class StartUp {
		#region Public Methods

		public void ConfigureInfrastructure(HttpConfiguration config) {
			JsonConvert.DefaultSettings = () => {
				return new JsonSerializerSettings {
					ContractResolver = new DefaultContractResolver {
						IgnoreSerializableAttribute = true
					}
				};
			};
		}

		#endregion
	}
}