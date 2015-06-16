using System;
using System.ComponentModel.DataAnnotations;

namespace Nameless.Spending.Core.Models.Views {
	[Serializable]
	public abstract class EntityViewModel {
		#region Public Properties

		[Key]
		public long ID { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }

		#endregion
	}
}