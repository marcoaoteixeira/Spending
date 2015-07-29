using System;
using AutoMapper;
using Nameless.Framework.Mapper;
using Nameless.Spending.Core.CommandQuery.Commands;

namespace Nameless.Spending.Core.Models.Bindings.Mappings {
	public class CreateBudgetMapperDefinition : AutoMapperMapperDefinition {
		#region Public Override Properties

		public override Type From {
			get { return typeof(BudgetBindingModel); }
		}

		public override Type To {
			get { return typeof(CreateBudgetCommand); }
		}
		
		#endregion

		#region Public Constructors

		public CreateBudgetMapperDefinition(IConfiguration configuration)
			: base(configuration) { }

		#endregion

		#region Public Override Methods

		public override void Create() {
			base.Create();

			Configuration.CreateMap<BudgetBindingModel, CreateBudgetCommand>();
		}

		#endregion
	}
}