using System;
using System.Collections.Generic;

namespace Nameless.Spending.Core.Models.Filters {
	public class BudgetFilterModel : FilterModel {
		#region Public Properties

		public string Description { get; set; }
		public int MinPeriodMonth { get; set; }
		public int MinPeriodYear { get; set; }
		public int MaxPeriodMonth { get; set; }
		public int MaxPeriodYear { get; set; }
		public decimal MinTotal { get; set; }
		public decimal MaxTotal { get; set; }
		public long CategoryID { get; set; }

		#endregion

		#region Public Methods

		public DateTime GetMinPeriod() {
			return new DateTime(1, MinPeriodMonth, MinPeriodYear);
		}

		public DateTime GetMaxPeriod() {
			return new DateTime(1, MaxPeriodMonth, MaxPeriodYear);
		}

		#endregion
	}
}