using System.Collections.Generic;
using System.Linq;

namespace Nameless.Spending.Core.Models {
	public class Category : Entity {
		#region	Private Read-Only Fields

		private readonly IList<Category> _ancestors = new List<Category>();

		#endregion

		#region	Public Properties

		public virtual string Description { get; set; }
		public virtual IList<Category> Ancestors {
			get { return _ancestors; }
		}

		#endregion
		
		#region Public Virtual Methods

		public virtual string GetBreadCrumb(string separator = " / ") {
			var descriptions = _ancestors
				.Select(ancestor => ancestor.Description)
				.Concat(new[] { Description })
				.ToArray();

			return string.Join(separator, descriptions);
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
	}
}