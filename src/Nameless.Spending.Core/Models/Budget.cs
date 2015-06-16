using System.Collections.Generic;
using System.Linq;

namespace Nameless.Spending.Core.Models {
	public class Budget : Entity {
		#region Private Read-Only Fields

#pragma warning disable 0649
		private readonly decimal _total;
#pragma warning restore 0649
		private readonly ICollection<BudgetItem> _items = new HashSet<BudgetItem>();

		#endregion

		#region Public Virtual Properties

		public virtual string Description { get; set; }
		public virtual BudgetPeriod Period { get; set; }
		public virtual Category Category { get; set; }
		public virtual ICollection<BudgetItem> Items {
			get { return _items; }
		}
		public virtual decimal Total {
			get { return _total; }
		}

		#endregion
	}
}