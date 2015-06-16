using System;

namespace Nameless.Spending.Core.Models.Views {
	public class DebitViewModel : OperationViewModel {
		#region Public Properties

		public long CategoryID { get; set; }
		public string CategoryDescription { get; set; }

		#endregion
	}
}