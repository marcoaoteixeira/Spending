using System;

namespace Nameless.Spending.Core.Models.Filters {
	public class BalancePerPeriodFilter {
		#region Public Properties

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		#endregion
	}
}