namespace Nameless.Spending.Core.Models {
	public class BudgetItem : Entity {
		#region Public Virtual Properties

		public virtual string Description { get; set; }
		public virtual decimal Value { get; set; }
		public virtual Budget Budget { get; set; }

		#endregion
	}
}