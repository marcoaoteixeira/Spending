using System;
using AutoMapper;
using Nameless.Framework.Mapper;
using Nameless.Spending.Core.CommandQuery.Commands;

namespace Nameless.Spending.Core.Models.Bindings.Mappings {
	public class AlterBudgetMapperDefinition : AutoMapperMapperDefinition {
		#region Public Override Properties

		public override Type From {
			get { return typeof(BudgetBindingModel); }
		}

		public override Type To {
			get { return typeof(AlterBudgetCommand); }
		}
		
		#endregion

		#region Public Constructors

		public AlterBudgetMapperDefinition(IConfiguration configuration)
			: base(configuration) { }

		#endregion

		#region Public Override Methods

		public override void Create() {
			base.Create();

			Configuration.CreateMap<BudgetBindingModel, AlterBudgetCommand>();
		}

		#endregion
	}
}