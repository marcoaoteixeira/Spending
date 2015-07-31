using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Core.Data.NHibernate.Mappings {
	public class CreditClassMapping : EntityClassMapping<Credit> {
		#region Public Constructors

		public CreditClassMapping()
			: base("credits", "credit_id") {
			Property(property => property.Description
				, mapping => {
					mapping.Column("description");
					mapping.Length(256);
					mapping.NotNullable(true);
					mapping.Index("credit_description_index");
				});

			Property(property => property.Value
				, mapping => {
					mapping.Column("value");
					mapping.Scale(3);
					mapping.Precision(16);
					mapping.NotNullable(true);
				});

			Property(property => property.Date
				, mapping => mapping.Column("date"));
		}

		#endregion
	}
}