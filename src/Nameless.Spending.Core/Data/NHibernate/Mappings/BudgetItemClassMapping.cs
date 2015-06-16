using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Core.Data.NHibernate.Mappings {
	public class BudgetItemClassMapping : EntityClassMapping<BudgetItem> {
		#region Public Constructors

		public BudgetItemClassMapping()
			: base("budget_items", "budget_item_id") {
			Property(property => property.Description
				, mapping => {
					mapping.Column("description");
					mapping.Length(256);
					mapping.NotNullable(true);
					mapping.Index("budget_item_description_index");
				});

			Property(property => property.Value
				, mapping => {
					mapping.Column("value");
					mapping.Scale(3);
					mapping.Precision(16);
					mapping.NotNullable(true);
				});

			ManyToOne(property => property.Budget
				, mapping => mapping.Column("budget_id"));
		}

		#endregion
	}
}