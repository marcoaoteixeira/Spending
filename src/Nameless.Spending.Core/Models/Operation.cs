using System;

namespace Nameless.Spending.Core.Models {
	public abstract class Operation : Entity {
		#region Public Virtual Properties

		public virtual string Description { get; set; }
		public virtual decimal Value { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual FundSource FundSource { get; set; }

		#endregion
	}
}