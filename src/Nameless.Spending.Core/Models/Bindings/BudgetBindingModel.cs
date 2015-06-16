using System.ComponentModel.DataAnnotations;
using Nameless.Spending.Core.Resources;

namespace Nameless.Spending.Core.Models.Bindings {
	public class BudgetBindingModel : EntityBindingModel {
		#region Public Properties

		[Display(ResourceType = typeof(Displays), Name = "Description")]
		[Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
		public string Description { get; set; }

		[Display(ResourceType = typeof(Displays), Name = "PeriodMonth")]
		[Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
		[Range(1, 12, ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Range")]
		public int? PeriodMonth { get; set; }

		[Display(ResourceType = typeof(Displays), Name = "PeriodYear")]
		[Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
		[Range(1, 9999, ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Range")]
		public int? PeriodYear { get; set; }

		[Display(ResourceType = typeof(Displays), Name = "Total")]
		public decimal Total { get; set; }

		[Display(ResourceType = typeof(Displays), Name = "CategoryID")]
		[Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
		public long? CategoryID { get; set; }

		#endregion
	}
}