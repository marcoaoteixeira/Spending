using Nameless.Framework.CommandQuery;
using Nameless.Framework.Data;
using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Core.CommandQuery.Commands {
	public class AlterBudgetCommand : ICommand {
		#region Public Properties

		public long BudgetID { get; set; }
		public string Description { get; set; }
		public int PeriodMonth { get; set; }
		public int PeriodYear { get; set; }
		public long CategoryID { get; set; }

		#endregion
	}

	public class AlterBudgetCommandHandler : CommandHandlerBase<AlterBudgetCommand> {
		#region Public Constructors

		public AlterBudgetCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(AlterBudgetCommand command) {
			var obj = Repository.FindOne<Budget>(_ => _.ID == command.BudgetID);

			if (obj == null) {
				throw new EntityNotFoundException(typeof(Budget));
			}

			obj.Description = command.Description;
			obj.Period = new BudgetPeriod {
				Month = command.PeriodMonth,
				Year = command.PeriodYear
			};
			obj.Category = Repository.Load<Category>(command.CategoryID);

			Repository.Store(obj);
		}

		#endregion
	}

	public class CreateBudgetCommand : ICommand {
		#region Public Properties

		public long BudgetID { get; set; }
		public string Description { get; set; }
		public int PeriodMonth { get; set; }
		public int PeriodYear { get; set; }
		public long CategoryID { get; set; }

		#endregion
	}

	public class CreateBudgetCommandHandler : CommandHandlerBase<CreateBudgetCommand> {
		#region Public Constructors

		public CreateBudgetCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(CreateBudgetCommand command) {
			var obj = new Budget {
				Description = command.Description,
				Period = new BudgetPeriod {
					Month = command.PeriodMonth,
					Year = command.PeriodYear
				},
				Category = Repository.Load<Category>(command.CategoryID)
			};

			Repository.Store(obj);

			command.BudgetID = obj.ID;
		}

		#endregion
	}

	public class DeleteBudgetCommand : ICommand {
		#region Public Properties

		public long BudgetID { get; set; }

		#endregion
	}

	public class DeleteBudgetCommandHandler : CommandHandlerBase<DeleteBudgetCommand> {
		#region Public Constructors

		public DeleteBudgetCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(DeleteBudgetCommand command) {
			var obj = Repository.FindOne<Budget>(_ =>
				_.ID == command.BudgetID);

			if (obj == null) {
				throw new EntityNotFoundException(typeof(Budget));
			}

			Repository.Delete(obj);
		}

		#endregion
	}

	public class CreateBudgetItemCommand : ICommand {
		#region Public Properties

		public long BudgetItemID { get; set; }
		public string Description { get; set; }
		public decimal Value { get; set; }
		public long BudgetID { get; set; }

		#endregion
	}

	public class CreateBudgetItemCommandHandler : CommandHandlerBase<CreateBudgetItemCommand> {
		#region Public Constructors

		public CreateBudgetItemCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(CreateBudgetItemCommand command) {
			var obj = new BudgetItem {
				Budget = Repository.Load<Budget>(command.BudgetID),
				Description = command.Description,
				Value = command.Value
			};

			Repository.Store(obj);

			command.BudgetItemID = obj.ID;
		}

		#endregion
	}

	public class DeleteBudgetItemCommand : ICommand {
		#region Public Properties

		public long BudgetID { get; set; }
		public long ItemID { get; set; }

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
}