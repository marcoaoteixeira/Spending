using System.Collections.Generic;
using System.Linq;

namespace Nameless.Spending.Core.Models.Views {
	public class FundSourceViewModel : EntityViewModel {
		#region Public Properties

		public string Name { get; set; }
		public IEnumerable<CreditViewModel> Credits { get; set; }
		public IEnumerable<DebitViewModel> Debits { get; set; }
		public decimal Balance { get; set; }

		#endregion

		#region Public Constructors

		public FundSourceViewModel() {
			Credits = Enumerable.Empty<CreditViewModel>();
			Debits = Enumerable.Empty<DebitViewModel>();
		}

		#endregion
	}
}