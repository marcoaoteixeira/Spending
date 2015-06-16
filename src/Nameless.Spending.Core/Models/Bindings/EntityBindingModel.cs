using System;
using System.ComponentModel.DataAnnotations;

namespace Nameless.Spending.Core.Models.Bindings {
	[Serializable]
	public abstract class EntityBindingModel {
		#region Public Properties

		[Key]
		public long ID { get; set; }

		#endregion
	}
}