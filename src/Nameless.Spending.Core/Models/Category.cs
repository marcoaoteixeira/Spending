using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Nameless.Spending.Core.Models {
	public class Category : Entity {
		#region	Private Read-Only Fields

		private readonly IList<Category> _ancestors = new ObservableCollection<Category>();

		#endregion

		#region Private Fields

		private string _breadCrumb;

		#endregion

		#region	Public Properties

		public virtual string Description { get; set; }
		public virtual IList<Category> Ancestors {
			get { return _ancestors; }
		}

		#endregion

		#region Public Constructors

		public Category() {
			((ObservableCollection<Category>)_ancestors).CollectionChanged += Ancestors_CollectionChanged;
		}

		#endregion

		#region Public Virtual Methods

		public virtual string GetBreadCrumb(string separator = " / ") {
			if (!string.IsNullOrWhiteSpace(_breadCrumb)) {
				return _breadCrumb;
			}
			
			var descriptions = _ancestors
				.Select(ancestor => ancestor.Description)
				.Concat(new[] { Description })
				.ToArray();

			return _breadCrumb = string.Join(separator, descriptions);
		}

		#endregion

		#region Public Virtual Methods

		public virtual bool Equals(Entity obj) {
			return obj != null && obj.ID == ID;
		}

		#endregion

		#region Public Override Methods

		public override bool Equals(object obj) {
			return Equals(obj as Entity);
		}

		public override int GetHashCode() {
			return ID.GetHashCode();
		}

		#endregion

		#region Private Methods

		private void Ancestors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			_breadCrumb = string.Empty;
		}

		#endregion
	}
}