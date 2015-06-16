using System.Collections.Generic;
using Nameless.Framework;
using Nameless.Framework.CommandQuery;
using Nameless.Framework.Data;
using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Core.CommandQuery.Commands {
	#region Commands

	public class DeleteCategoryCommand : ICommand {
		#region Public Properties

		public long ID { get; set; }

		#endregion
	}

	public class StoreCategoryCommand : ICommand {
		#region Public Properties

		public long ID { get; set; }
		public string Description { get; set; }
		public IEnumerable<long> Ancestors { get; set; }

		#endregion
	}

	#endregion

	#region Handlers

	public class DeleteCategoryCommandHandler : CommandHandlerBase<DeleteCategoryCommand> {
		#region Public Constructors

		public DeleteCategoryCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(DeleteCategoryCommand command) {
			var category = Repository.FindOne<Category>(_ => _.ID == command.ID);

			if (category == null) {
				throw new EntityNotFoundException(typeof(Category));
			}

			Repository.Delete(category);
		}

		#endregion
	}

	public class StoreCategoryCommandHandler : CommandHandlerBase<StoreCategoryCommand> {
		#region Public Constructors

		public StoreCategoryCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(StoreCategoryCommand command) {
			var obj = Repository.FindOne<Category>(_ => _.ID == command.ID) ?? new Category();

			obj.Description = command.Description;
			obj.Ancestors.Clear();

			command.Ancestors.Each(ancestor => {
				obj.Ancestors.Add(Repository.Load<Category>(ancestor));
			});

			Repository.Store(obj);

			command.ID = obj.ID;
		}

		#endregion
	}

	#endregion
}