using System;

namespace Nameless.Spending.Core.Models.Views {
	public abstract class OperationViewModel : EntityViewModel {
		#region Public Properties

		public string Description { get; set; }
		public decimal Value { get; set; }
		public DateTime Date { get; set; }

		#endregion
	}
}