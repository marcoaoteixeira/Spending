using System;
using AutoMapper;
using Nameless.Framework.Mapper;

namespace Nameless.Spending.Core.Models.Views.Mappings {
	public class BudgetMapperDefinition : AutoMapperMapperDefinition {
		#region Public Override Properties

		public override Type From {
			get { return typeof(Budget); }
		}

		public override Type To {
			get { return typeof(BudgetViewModel); }
		}
		
		#endregion

		#region Public Constructors

		public BudgetMapperDefinition(IConfiguration configuration)
			: base(configuration) { }

		#endregion
	}
}