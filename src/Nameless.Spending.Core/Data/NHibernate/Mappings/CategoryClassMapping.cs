using Nameless.Spending.Core.Models;
using NHibernate.Mapping.ByCode;

namespace Nameless.Spending.Core.Data.NHibernate.Mappings {
	public sealed class CategoryClassMapping : EntityClassMapping<Category> {
		#region Public Constructors

		public CategoryClassMapping()
			: base("categories", "category_id") {
			Property(property => property.Description
				, mapping => {
					mapping.Column("description");
					mapping.NotNullable(true);
					mapping.Length(256);
					mapping.Index("category_description_index");
				});

			List(property => property.Ancestors
				, collectionMapping => {
					collectionMapping.Table("category_ancestors");
					collectionMapping.Access(Accessor.Field);
					collectionMapping.Cascade(Cascade.All.Include(Cascade.DeleteOrphans));
					collectionMapping.Key(key => key.Column("category_id"));
					collectionMapping.Lazy(CollectionLazy.NoLazy);
				}
				, mapping =>
					mapping.ManyToMany(manyToManyMapping =>
						manyToManyMapping.Column("ancestor_id")));
		}

		#endregion
	}
}