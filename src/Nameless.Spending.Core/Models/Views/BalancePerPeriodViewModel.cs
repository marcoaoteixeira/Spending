using System;

namespace Nameless.Spending.Core.Models.Views {
	public class BalancePerPeriodViewModel {
		#region Public Properties

		public DateTime Period { get; set; }
		public decimal TotalCredits { get; set; }
		public decimal TotalDebits { get; set; }

		#endregion
	}
}