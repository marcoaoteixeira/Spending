using Nameless.Spending.Core.Models;
using NHibernate.Mapping.ByCode;

namespace Nameless.Spending.Core.Data.NHibernate.Mappings {
	public class FundSourceClassMapping : EntityClassMapping<FundSource> {
		#region Public Constructors

		public FundSourceClassMapping()
			: base("fund_sources", "fund_source_id") {
			Property(property => property.Name
				, mapping => {
					mapping.Column("name");
					mapping.Length(256);
					mapping.NotNullable(true);
					mapping.Index("fund_source_name_index");
				});

			Set(property => property.Credits,
				collectionMapping => {
					collectionMapping.Access(Accessor.Field);
					collectionMapping.Cascade(Cascade.All);
					collectionMapping.Key(keyMapping => keyMapping.Column("fund_source_id"));
				}, mapping => mapping.OneToMany());

			Set(property => property.Debits,
				collectionMapping => {
					collectionMapping.Access(Accessor.Field);
					collectionMapping.Cascade(Cascade.All);
					collectionMapping.Key(keyMapping => keyMapping.Column("fund_source_id"));
				}, mapping => mapping.OneToMany());
		}

		#endregion
	}
}