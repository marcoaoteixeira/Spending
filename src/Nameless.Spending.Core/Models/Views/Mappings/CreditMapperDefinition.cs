using System;
using AutoMapper;
using Nameless.Framework.Mapper;

namespace Nameless.Spending.Core.Models.Views.Mappings {
	public class CreditMapperDefinition : AutoMapperMapperDefinition {
		#region Public Override Properties

		public override Type From {
			get { return typeof(Credit); }
		}

		public override Type To {
			get { return typeof(CreditViewModel); }
		}
		
		#endregion

		#region Public Constructors

		public CreditMapperDefinition(IConfiguration configuration)
			: base(configuration) { }

		#endregion
	}
}