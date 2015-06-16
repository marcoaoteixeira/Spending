namespace Nameless.Spending.Core.Models.Views {
	public class BudgetItemViewModel : EntityViewModel {
		#region Public Methods

		public string Description { get; set; }
		public decimal Value { get; set; }
		public long BudgetID { get; set; }
		public string BudgetDescription { get; set; }

		#endregion
	}
}