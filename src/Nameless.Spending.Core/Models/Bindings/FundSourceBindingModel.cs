using System.ComponentModel.DataAnnotations;
using Nameless.Spending.Core.Resources;

namespace Nameless.Spending.Core.Models.Bindings {
	public class FundSourceBindingModel : EntityBindingModel {
		#region Public Properties

		[Display(ResourceType = typeof(Displays), Name = "Name")]
		[Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
		[StringLength(256, ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "StringLength", MinimumLength = 1)]
		public string Name { get; set; }

		#endregion
	}
}