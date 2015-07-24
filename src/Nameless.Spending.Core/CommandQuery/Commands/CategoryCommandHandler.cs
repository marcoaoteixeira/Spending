using System.Collections.Generic;
using Nameless.Framework;
using Nameless.Framework.CommandQuery;
using Nameless.Framework.Data;
using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Core.CommandQuery.Commands {
	public class AlterCategoryCommand : ICommand {
		#region Public Properties

		public long CategoryID { get; set; }
		public string Description { get; set; }
		public IEnumerable<long> Ancestors { get; set; }

		#endregion
	}

	public class AlterCategoryCommandHandler : CommandHandlerBase<AlterCategoryCommand> {
		#region Public Constructors

		public AlterCategoryCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(AlterCategoryCommand command) {
			var obj = Repository.FindOne<Category>(_ => _.ID == command.CategoryID);

			if (obj == null) {
				throw new EntityNotFoundException(typeof(Category));
			}

			obj.Description = command.Description;
			obj.Ancestors.Clear();

			command.Ancestors.Each(ancestor => {
				obj.Ancestors.Add(Repository.Load<Category>(ancestor));
			});

			Repository.Store(obj);
		}

		#endregion
	}

	public class CreateCategoryCommand : ICommand {
		#region Public Properties

		public long CategoryID { get; set; }
		public string Description { get; set; }
		public IEnumerable<long> Ancestors { get; set; }

		#endregion
	}

	public class CreateCategoryCommandHandler : CommandHandlerBase<CreateCategoryCommand> {
		#region Public Constructors

		public CreateCategoryCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(CreateCategoryCommand command) {
			var obj = new Category {
				Description = command.Description
			};

			command.Ancestors.Each(ancestor => {
				obj.Ancestors.Add(Repository.Load<Category>(ancestor));
			});

			Repository.Store(obj);

			command.CategoryID = obj.ID;
		}

		#endregion
	}

	public class DeleteCategoryCommand : ICommand {
		#region Public Properties

		public long CategoryID { get; set; }

		#endregion
	}

	public class DeleteCategoryCommandHandler : CommandHandlerBase<DeleteCategoryCommand> {
		#region Public Constructors

		public DeleteCategoryCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(DeleteCategoryCommand command) {
			var category = Repository.FindOne<Category>(_ => _.ID == command.CategoryID);

			if (category == null) {
				throw new EntityNotFoundException(typeof(Category));
			}

			Repository.Delete(category);
		}

		#endregion
	}
}