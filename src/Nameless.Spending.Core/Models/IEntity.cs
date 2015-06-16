using System;

namespace Nameless.Spending.Core.Models {
	public interface IEntity {
		#region Properties

		long ID { get; }
		DateTime DateCreated { get; }
		DateTime DateModified { get; }

		#endregion
	}

	public abstract class Entity : IEntity {
		#region Private Read-Only Fields

#pragma warning disable 0649
		private readonly long _id;
		private readonly DateTime _dateCreated;
		private readonly DateTime _dateModified;
#pragma warning restore 0649

		#endregion

		#region IEntity Members

		public virtual long ID {
			get { return _id; }
		}
		public virtual DateTime DateCreated {
			get { return _dateCreated; }
		}
		public virtual DateTime DateModified {
			get { return _dateModified; }
		}

		#endregion
	}
}