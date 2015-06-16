using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Nameless.Spending.Core.Resources;

namespace Nameless.Spending.Core.Models.Bindings {
	public class CategoryBindingModel : EntityBindingModel {
		#region Public Properties

		[Display(ResourceType = typeof(Displays), Name = "Description")]
		[Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
		[StringLength(256, ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "StringLength", MinimumLength = 1)]
		public string Description { get; set; }

		[Display(ResourceType = typeof(Displays), Name = "Ancestors")]
		public IEnumerable<long> Ancestors { get; set; }

		#endregion

		#region Public Constructors

		public CategoryBindingModel() {
			Ancestors = Enumerable.Empty<long>();
		}

		#endregion
	}
}