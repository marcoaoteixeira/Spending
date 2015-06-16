using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Core.Data.NHibernate.Mappings {
	public class DebitClassMapping : EntityClassMapping<Debit> {
		#region Public Constructors

		public DebitClassMapping()
			: base("debits", "debit_id") {
			Property(property => property.Description
				, mapping => {
					mapping.Column("description");
					mapping.Length(256);
					mapping.NotNullable(true);
					mapping.Index("debit_description_index");
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

			ManyToOne(property => property.FundSource
				, mapping => mapping.Column("fund_source_id"));

			ManyToOne(property => property.Category
				, mapping => mapping.Column("category_id"));
		}

		#endregion
	}
}