using System;

namespace Nameless.Spending.Core.Models {
	public abstract class Operation : Entity {
		#region Public Virtual Properties

		public virtual string Description { get; set; }
		public virtual decimal Value { get; set; }
		public virtual DateTime Date { get; set; }

		#endregion

		#region Public Virtual Properties

		public virtual string GetPeriod() {
			return string.Format("{0}-{1}", Date.Year, Date.Month.ToString().PadLeft(2, '0'));
		}

		#endregion
	}
}