using System.Collections.Generic;
using System.Linq;

namespace Nameless.Spending.Core.Models.Views {
	public class BudgetViewModel : EntityViewModel {
		#region Public Properties

		public string Description { get; set; }
		public int PeriodMonth { get; set; }
		public int PeriodYear { get; set; }
		public IEnumerable<BudgetItemViewModel> Items { get; set; }
		public decimal Total { get; set; }
		public long CategoryID { get; set; }
		public string CategoryDescription { get; set; }

		#endregion

		#region Public Constructors

		public BudgetViewModel() {
			Items = Enumerable.Empty<BudgetItemViewModel>();
		}

		#endregion
	}
}