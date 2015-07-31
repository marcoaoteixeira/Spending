using System;
using System.ComponentModel.DataAnnotations;
using Nameless.Spending.Core.Resources;

namespace Nameless.Spending.Core.Models.Bindings {
	public abstract class OperationBindingModel : EntityBindingModel {
		#region Public Properties

		[Display(ResourceType = typeof(Displays), Name = "Description")]
		[Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
		public string Description { get; set; }

		[Display(ResourceType = typeof(Displays), Name = "Value")]
		public decimal Value { get; set; }

		[Display(ResourceType = typeof(Displays), Name = "Date")]
		[Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
		public DateTime? Date { get; set; }

		#endregion
	}
}