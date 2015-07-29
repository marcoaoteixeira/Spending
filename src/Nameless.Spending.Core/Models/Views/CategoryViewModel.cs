using System.ComponentModel.DataAnnotations;
using Nameless.Spending.Core.Resources;

namespace Nameless.Spending.Core.Models.Views {
	public class CategoryViewModel : EntityViewModel {
		#region Public Properties

		[Display(ResourceType = typeof(Displays), Name = "Description")]
		public string Description { get; set; }
		[Display(ResourceType = typeof(Displays), Name = "BreadCrumb")]
		public string BreadCrumb { get; set; }

		#endregion
	}
}