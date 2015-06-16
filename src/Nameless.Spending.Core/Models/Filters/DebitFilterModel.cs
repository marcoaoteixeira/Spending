using System;

namespace Nameless.Spending.Core.Models.Filters {
	public class DebitFilterModel : FilterModel {
		#region Public Properties

		public string Description { get; set; }
		public decimal MinValue { get; set; }
		public decimal MaxValue { get; set; }
		public DateTime MinDate { get; set; }
		public DateTime MaxDate { get; set; }
		public long CategoryID { get; set; }
		public long FundSourceID { get; set; }

		#endregion
	}
}