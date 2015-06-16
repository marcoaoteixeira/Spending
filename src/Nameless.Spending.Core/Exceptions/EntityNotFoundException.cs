using System;
using System.Runtime.Serialization;
using Nameless.Spending.Core.Resources;

namespace Nameless.Spending.Core {
	[Serializable]
	public class EntityNotFoundException : Exception {
		#region Public Constructors

		public EntityNotFoundException(Type entityType)
			: this(string.Format(Strings.EntityNotFoundExceptionMessageFormatter, entityType)) { }

		public EntityNotFoundException()
			: this(Strings.EntityNotFoundExceptionDefaultMessage) { }

		public EntityNotFoundException(string message)
			: base(message) { }

		public EntityNotFoundException(string message, Exception inner)
			: base(message, inner) { }

		#endregion

		#region Protected Constructors

		protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }

		#endregion
	}
}