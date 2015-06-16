using Nameless.Framework.CommandQuery;
using Nameless.Framework.Data;
using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Core.CommandQuery.Commands {
	#region Commands

	public class DeleteBudgetCommand : ICommand {
		#region Public Properties

		public long ID { get; set; }

		#endregion
	}

	public class StoreBudgetCommand : ICommand {
		#region Public Properties

		public long ID { get; set; }
		public string Description { get; set; }
		public int PeriodMonth { get; set; }
		public int PeriodYear { get; set; }
		public long CategoryID { get; set; }

		#endregion
	}

	public class DeleteBudgetItemCommand : ICommand {
		#region Public Properties

		public long BudgetID { get; set; }
		public long ItemID { get; set; }

		#endregion
	}

	public class StoreBudgetItemCommand : ICommand {
		#region Public Properties

		public long ID { get; set; }
		public string Description { get; set; }
		public decimal Value { get; set; }
		public long BudgetID { get; set; }

		#endregion
	}

	#endregion

	#region Handlers

	public class DeleteBudgetCommandHandler : CommandHandlerBase<DeleteBudgetCommand> {
		#region Public Constructors

		public DeleteBudgetCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(DeleteBudgetCommand command) {
			var budget = Repository.FindOne<Budget>(_ => _.ID == command.ID);

			if (budget == null) {
				throw new EntityNotFoundException(typeof(Budget));
			}

			Repository.Delete(budget);
		}

		#endregion
	}

	public class StoreBudgetCommandHandler : CommandHandlerBase<StoreBudgetCommand> {
		#region Public Constructors

		public StoreBudgetCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(StoreBudgetCommand command) {
			var obj = Repository.FindOne<Budget>(_ => _.ID == command.ID) ?? new Budget();

			obj.Description = command.Description;
			obj.Period = new BudgetPeriod {
				Month = command.PeriodMonth,
				Year = command.PeriodYear
			};
			obj.Category = Repository.Load<Category>(command.CategoryID);

			Repository.Store(obj);

			command.ID = obj.ID;
		}

		#endregion
	}

	public class DeleteBudgetItemCommandHandler : CommandHandlerBase<DeleteBudgetItemCommand> {
		#region Public Constructors

		public DeleteBudgetItemCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(DeleteBudgetItemCommand command) {
			var obj = Repository.FindOne<BudgetItem>(_ =>
				_.Budget.ID == command.BudgetID &&
				_.ID == command.ItemID);

			if (obj == null) {
				throw new EntityNotFoundException(typeof(BudgetItem));
			}

			Repository.Delete(obj);
		}

		#endregion
	}

	public class StoreBudgetItemCommandHandler : CommandHandlerBase<StoreBudgetItemCommand> {
		#region Public Constructors

		public StoreBudgetItemCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(StoreBudgetItemCommand command) {
			var obj = new BudgetItem {
				Budget = Repository.Load<Budget>(command.BudgetID),
				Description = command.Description,
				Value = command.Value
			};

			Repository.Store(obj);

			command.ID = obj.ID;
		}

		#endregion
	}

	#endregion
}