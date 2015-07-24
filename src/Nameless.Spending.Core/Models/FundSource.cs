using System.Collections.Generic;
using System.Linq;

namespace Nameless.Spending.Core.Models {
	public class FundSource : Entity {
		#region Private Read-Only Fields

#pragma warning disable 0649
		private readonly decimal _balance;
#pragma warning restore 0649
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
			get { return _balance; }
		}

		#endregion
	}
}