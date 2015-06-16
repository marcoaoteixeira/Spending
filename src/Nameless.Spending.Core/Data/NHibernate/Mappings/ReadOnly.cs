using NHibernate.Type;

namespace Nameless.Spending.Core.Data.NHibernate.Mappings {
	internal static class ReadOnly {
		#region Public Static Read-Only Fields

		public static readonly IIdentifierType Int64 = new Int64Type();

		#endregion
	}
}