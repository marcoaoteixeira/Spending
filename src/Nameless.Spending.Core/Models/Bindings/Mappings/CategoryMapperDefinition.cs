using System;
using AutoMapper;
using Nameless.Framework.Mapper;
using Nameless.Spending.Core.CommandQuery.Commands;

namespace Nameless.Spending.Core.Models.Bindings.Mappings {
	public class CategoryMapperDefinition : AutoMapperMapperDefinition {
		#region Public Override Properties

		public override Type From {
			get { return typeof(CategoryBindingModel); }
		}

		public override Type To {
			get { return typeof(StoreCategoryCommand); }
		}
		
		#endregion

		#region Public Constructors

		public CategoryMapperDefinition(IConfiguration configuration)
			: base(configuration) { }

		#endregion

		#region Public Override Methods

		public override void Create() {
			base.Create();

			Configuration.CreateMap<CategoryBindingModel, StoreCategoryCommand>();
		}

		#endregion
	}
}