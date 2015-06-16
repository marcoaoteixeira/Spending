namespace Nameless.Spending.Core.Models.Filters {
	public class BudgetItemFilterModel : FilterModel {
		#region Public Methods

		public string Description { get; set; }
		public decimal MinValue { get; set; }
		public decimal MaxValue { get; set; }
		public long CategoryID { get; set; }
		public long BudgetID { get; set; }

		#endregion
	}
}