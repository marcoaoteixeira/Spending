using System;
using AutoMapper;
using Nameless.Framework.Mapper;
using Nameless.Spending.Core.CommandQuery.Commands;

namespace Nameless.Spending.Core.Models.Bindings.Mappings {
	public class DebitMapperDefinition : AutoMapperMapperDefinition {
		#region Public Override Properties

		public override Type From {
			get { return typeof(DebitBindingModel); }
		}

		public override Type To {
			get { return typeof(AlterDebitCommand); }
		}
		
		#endregion

		#region Public Constructors

		public DebitMapperDefinition(IConfiguration configuration)
			: base(configuration) { }

		#endregion

		#region Public Override Methods

		public override void Create() {
			base.Create();

			Configuration.CreateMap<DebitBindingModel, AlterDebitCommand>();
		}

		#endregion
	}
}