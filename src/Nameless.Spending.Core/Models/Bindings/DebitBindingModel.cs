using System.ComponentModel.DataAnnotations;
using Nameless.Spending.Core.Resources;

namespace Nameless.Spending.Core.Models.Bindings {
	public class DebitBindingModel : OperationBindingModel {
		#region Public Properties

		[Display(ResourceType = typeof(Displays), Name = "CategoryID")]
		[Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
		public long CategoryID { get; set; }

		#endregion
	}
}