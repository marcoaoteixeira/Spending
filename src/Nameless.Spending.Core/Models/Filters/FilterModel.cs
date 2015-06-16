using System;

namespace Nameless.Spending.Core.Models.Filters {
	public abstract class FilterModel {
		#region Private Fields

		private int _pageNumber;
		private int _pageSize;

		#endregion

		#region Public Properties

		public long ID { get; set; }
		public DateTime MinDateCreated { get; set; }
		public DateTime MaxDateCreated { get; set; }
		public DateTime MinDateModified { get; set; }
		public DateTime MaxDateModified { get; set; }
		public bool DisablePaginate { get; set; }
		public int PageNumber {
			get { return _pageNumber > 0 ? _pageNumber : 1; }
			set { _pageNumber = value > 0 ? value : 1; }
		}
		public int PageSize {
			get { return _pageSize > 0 ? _pageSize : 10; }
			set { _pageSize = value > 0 ? value : 10; }
		}

		#endregion
	}
}