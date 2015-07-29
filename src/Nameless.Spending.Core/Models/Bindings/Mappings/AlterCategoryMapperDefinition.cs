using System;
using AutoMapper;
using Nameless.Framework.Mapper;
using Nameless.Spending.Core.CommandQuery.Commands;

namespace Nameless.Spending.Core.Models.Bindings.Mappings {
	public class AlterCategoryMapperDefinition : AutoMapperMapperDefinition {
		#region Public Override Properties

		public override Type From {
			get { return typeof(CategoryBindingModel); }
		}

		public override Type To {
			get { return typeof(AlterCategoryCommand); }
		}
		
		#endregion

		#region Public Constructors

		public AlterCategoryMapperDefinition(IConfiguration configuration)
			: base(configuration) { }

		#endregion

		#region Public Override Methods

		public override void Create() {
			base.Create();

			Configuration.CreateMap<CategoryBindingModel, AlterCategoryCommand>();
		}

		#endregion
	}
}