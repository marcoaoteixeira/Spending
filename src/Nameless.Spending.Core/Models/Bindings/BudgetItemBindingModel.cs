using System.ComponentModel.DataAnnotations;
using Nameless.Spending.Core.Resources;

namespace Nameless.Spending.Core.Models.Bindings {
	public class BudgetItemBindingModel : EntityBindingModel {
		#region Public Methods

		[Display(ResourceType = typeof(Displays), Name = "Description")]
		[Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
		public string Description { get; set; }

		[Display(ResourceType = typeof(Displays), Name = "Value")]
		[Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
		public decimal? Value { get; set; }

		[Display(ResourceType = typeof(Displays), Name = "BudgetID")]
		[Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
		public long? BudgetID { get; set; }

		#endregion
	}
}