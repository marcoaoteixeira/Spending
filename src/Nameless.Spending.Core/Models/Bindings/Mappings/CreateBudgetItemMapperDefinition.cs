using System;
using AutoMapper;
using Nameless.Framework.Mapper;
using Nameless.Spending.Core.CommandQuery.Commands;

namespace Nameless.Spending.Core.Models.Bindings.Mappings {
	public class CreateBudgetItemMapperDefinition : AutoMapperMapperDefinition {
		#region Public Override Properties

		public override Type From {
			get { return typeof(BudgetItemBindingModel); }
		}

		public override Type To {
			get { return typeof(CreateBudgetItemCommand); }
		}
		
		#endregion

		#region Public Constructors

		public CreateBudgetItemMapperDefinition(IConfiguration configuration)
			: base(configuration) { }

		#endregion

		#region Public Override Methods

		public override void Create() {
			base.Create();

			Configuration.CreateMap<BudgetItemBindingModel, CreateBudgetItemCommand>();
		}

		#endregion
	}
}