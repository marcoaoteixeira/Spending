using System;

namespace Nameless.Spending.Core.Models.Views {
	public class DebitPerCategoryViewModel {
		#region Public Properties

		public long CategoryID { get; set; }
		public string CategoryDescription { get; set; }
		public decimal TotalDebits { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		#endregion
	}
}