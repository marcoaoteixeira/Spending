using System.Collections.Generic;
using System.Linq;

namespace Nameless.Spending.Core.Models {
	public class FundSource : Entity {
		#region Private Read-Only Fields

		private readonly ICollection<Credit> _credits = new HashSet<Credit>();
		private readonly ICollection<Debit> _debits = new HashSet<Debit>();

		#endregion

		#region Public Virtual Properties

		public virtual string Name { get; set; }
		public virtual ICollection<Credit> Credits {
			get { return _credits; }
		}
		public virtual ICollection<Debit> Debits {
			get { return _debits; }
		}
		public virtual decimal Balance {
			get { return GetBalance(); }
		}

		#endregion

		#region Private Methods

		private decimal GetBalance() {
			return Credits.Sum(_ => _.Value) - Debits.Sum(_ => _.Value);
		}

		#endregion
	}
}