using System;
using AutoMapper;
using Nameless.Framework.Mapper;
using Nameless.Spending.Core.CommandQuery.Commands;

namespace Nameless.Spending.Core.Models.Bindings.Mappings {
	public class CreditMapperDefinition : AutoMapperMapperDefinition {
		#region Public Override Properties

		public override Type From {
			get { return typeof(CreditBindingModel); }
		}

		public override Type To {
			get { return typeof(AlterCreditCommand); }
		}
		
		#endregion

		#region Public Constructors

		public CreditMapperDefinition(IConfiguration configuration)
			: base(configuration) { }

		#endregion

		#region Public Override Methods

		public override void Create() {
			base.Create();

			Configuration.CreateMap<CreditBindingModel, AlterCreditCommand>()
				.ForMember(command => command.AlterFundSourceID, options => options.MapFrom(source => source.FundSourceID));
		}

		#endregion
	}
}