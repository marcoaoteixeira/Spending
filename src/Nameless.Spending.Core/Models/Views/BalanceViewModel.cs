namespace Nameless.Spending.Core.Models.Views {
	public class BalanceViewModel {
		#region Public Properties

		public decimal TotalCredits { get; set; }
		public decimal TotalDebits { get; set; }
		public decimal CurrentBalance {
			get { return TotalCredits - TotalDebits; }
		}

		#endregion
	}
}