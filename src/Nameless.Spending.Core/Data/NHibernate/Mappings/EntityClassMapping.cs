using Nameless.Spending.Core.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Nameless.Spending.Core.Data.NHibernate.Mappings {
	public abstract class EntityClassMapping<TEntity> : ClassMapping<TEntity>
		where TEntity : Entity {
		#region Protected Constructors

		protected EntityClassMapping(string tableName, string keyName) {
			Table(tableName);

			Id(property => property.ID
				, idMapper => {
					idMapper.Access(Accessor.Field);
					idMapper.Column(keyName);
					idMapper.Generator(Generators.Native);
					idMapper.Type(ReadOnly.Int64);
				});

			Property(property => property.DateCreated
				, mapping => {
					mapping.Access(Accessor.Field);
					mapping.Column("date_created");
				});

			Property(property => property.DateModified
				, mapping => {
					mapping.Access(Accessor.Field);
					mapping.Column("date_modified");
				});
		}

		#endregion
	}
}