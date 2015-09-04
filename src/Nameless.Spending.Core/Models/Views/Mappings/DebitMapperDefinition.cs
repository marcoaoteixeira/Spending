using System;
using AutoMapper;
using Nameless.Framework.Mapper;

namespace Nameless.Spending.Core.Models.Views.Mappings {
	public class DebitMapperDefinition : AutoMapperMapperDefinition {
		#region Public Override Properties

		public override Type From {
			get { return typeof(Debit); }
		}

		public override Type To {
			get { return typeof(DebitViewModel); }
		}
		
		#endregion

		#region Public Constructors

		public DebitMapperDefinition(IConfiguration configuration)
			: base(configuration) { }

		#endregion
	}
}