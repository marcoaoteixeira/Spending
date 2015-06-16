namespace Nameless.Spending.Core.Models {
	public class Debit : Operation {
		#region Public Virtual Properties

		public virtual Category Category { get; set; }

		#endregion
	}
}