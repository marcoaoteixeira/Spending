using System;
using AutoMapper;
using Nameless.Framework.Mapper;

namespace Nameless.Spending.Core.Models.Views.Mappings {
	public class CategoryMapperDefinition : AutoMapperMapperDefinition {
		#region Public Override Properties

		public override Type From {
			get { return typeof(Category); }
		}

		public override Type To {
			get { return typeof(CategoryViewModel); }
		}
		
		#endregion

		#region Public Constructors

		public CategoryMapperDefinition(IConfiguration configuration)
			: base(configuration) { }

		#endregion

		#region Public Override Methods

		public override void Create() {
			base.Create();

			Configuration.CreateMap<Category, CategoryViewModel>()
				.ForMember(member => member.BreadCrumb, options => options.MapFrom(category => category.GetBreadCrumb(" / ")));
		}

		#endregion
	}
}