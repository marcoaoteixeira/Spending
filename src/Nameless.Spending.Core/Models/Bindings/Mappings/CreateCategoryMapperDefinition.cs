using System;
using AutoMapper;
using Nameless.Framework.Mapper;
using Nameless.Spending.Core.CommandQuery.Commands;

namespace Nameless.Spending.Core.Models.Bindings.Mappings {
	public class CreateCategoryMapperDefinition : AutoMapperMapperDefinition {
		#region Public Override Properties

		public override Type From {
			get { return typeof(CategoryBindingModel); }
		}

		public override Type To {
			get { return typeof(CreateCategoryCommand); }
		}
		
		#endregion

		#region Public Constructors

		public CreateCategoryMapperDefinition(IConfiguration configuration)
			: base(configuration) { }

		#endregion

		#region Public Override Methods

		public override void Create() {
			base.Create();

			Configuration.CreateMap<CategoryBindingModel, CreateCategoryCommand>();
		}

		#endregion
	}
}