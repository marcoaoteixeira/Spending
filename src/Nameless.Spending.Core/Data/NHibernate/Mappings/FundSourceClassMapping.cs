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

			Property(property => property.TotalCredit,
				mapping => {
					mapping.Access(Accessor.Field);
					mapping.Formula(GetTotalCreditQuery());
				});

			Property(property => property.TotalDebit,
				mapping => {
					mapping.Access(Accessor.Field);
					mapping.Formula(GetTotalDebitQuery());
				});
		}

		#endregion

		#region Private Static Methods

		// Note: This SQL syntax is for SQLite. If you plan to change database, review this script snippet.
		private static string GetBalanceQuery() {
			return @"(SELECT
							COALESCE((SUM(credits.value) - SUM(debits.value)), 0.000)
						FROM fund_sources
							INNER JOIN credits on credits.fund_source_id = fund_sources.fund_source_id
							INNER JOIN debits on debits.fund_source_id = fund_sources.fund_source_id
						WHERE fund_sources.fund_source_id = fund_source_id)";
		}

		private static string GetTotalCreditQuery() {
			return @"(SELECT
							COALESCE(SUM(credits.value), 0.000)
						FROM fund_sources
							INNER JOIN credits on credits.fund_source_id = fund_sources.fund_source_id
						WHERE fund_sources.fund_source_id = fund_source_id)";
		}

		private static string GetTotalDebitQuery() {
			return @"(SELECT
							COALESCE(SUM(debits.value), 0.000)
						FROM fund_sources
							INNER JOIN debits on debits.fund_source_id = fund_sources.fund_source_id
						WHERE fund_sources.fund_source_id = fund_source_id)";
		}

		#endregion
	}
}