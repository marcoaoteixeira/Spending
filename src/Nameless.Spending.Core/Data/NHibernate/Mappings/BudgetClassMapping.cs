using Nameless.Spending.Core.Models;
using NHibernate.Mapping.ByCode;

namespace Nameless.Spending.Core.Data.NHibernate.Mappings {
	public class BudgetClassMapping : EntityClassMapping<Budget> {
		#region Public Constructors

		public BudgetClassMapping()
			: base("budgets", "budget_id") {
			Property(property => property.Description
				, mapping => {
					mapping.Column("description");
					mapping.Length(256);
					mapping.NotNullable(true);
					mapping.Index("budget_description_index");
				});

			Component(property => property.Period
				, mapping => {
					mapping.Property(mappingProperty => mappingProperty.Month
						, propertyMapping => propertyMapping.Column("month"));
					mapping.Property(mappingProperty => mappingProperty.Year
						, propertyMapping => propertyMapping.Column("year"));
				});

			ManyToOne(property => property.Category
				, mapping => mapping.Column("category_id"));

			Set(property => property.Items,
				collectionMapping => {
					collectionMapping.Access(Accessor.Field);
					collectionMapping.Cascade(Cascade.All);
					collectionMapping.Key(keyMapping => keyMapping.Column("budget_id"));
				}, mapping => mapping.OneToMany());

			Property(property => property.Total,
				mapping => {
					mapping.Access(Accessor.Field);
					mapping.Formula("(select sum(budget_items.value) from budget_items where budget_items.budget_id = budget_id)");
				});
		}

		#endregion
	}
}