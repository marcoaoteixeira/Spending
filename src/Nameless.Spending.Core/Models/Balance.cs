namespace Nameless.Spending.Core.Models {
	public class Balance {
		#region Public Virtual Properties

		public virtual decimal TotalCredit { get; set; }
		public virtual decimal TotalDebit { get; set; }
		public virtual decimal CurrentBalance { get; set; }

		#endregion
	}
}