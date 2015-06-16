using System;
namespace Nameless.Spending.Core.Models {
	public class BudgetPeriod {
		#region Public Virtual Properties

		public virtual int Month { get; set; }
		public virtual int Year { get; set; }

		#endregion

		#region Public Virtual Methods

		public virtual bool Equals(BudgetPeriod obj) {
			return obj != null &&
				   obj.Month == Month &&
				   obj.Year == Year;
		}

		#endregion

		#region Public Override Methods

		public override bool Equals(object obj) {
			return Equals(obj as BudgetPeriod);
		}

		public override int GetHashCode() {
			return Month.GetHashCode() + Year.GetHashCode();
		}

		public override string ToString() {
			return string.Format("{0}/{1}", Month.ToString().PadLeft(2, '0'), Year);
		}

		#endregion

		#region Public Static Operators

		public static implicit operator DateTime(BudgetPeriod period) {
			return new DateTime(1, period.Month, period.Year);
		}

		public static implicit operator BudgetPeriod(DateTime dateTime) {
			return new BudgetPeriod { Month = dateTime.Month, Year = dateTime.Year };
		}

		//public static explicit operator DateTime(BudgetPeriod period) {
		//	return new DateTime(1, period.Month, period.Year);
		//}

		//public static explicit operator BudgetPeriod(DateTime dateTime) {
		//	return new BudgetPeriod { Month = dateTime.Month, Year = dateTime.Year };
		//}

		#endregion
	}
}