using System;
using Nameless.Framework.CommandQuery;
using Nameless.Framework.Data;
using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Core.CommandQuery.Commands {
	public class DeleteDebitCommand : ICommand {
		#region Public Properties

		public long ID { get; set; }

		#endregion
	}

	public class DeleteDebitCommandHandler : CommandHandlerBase<DeleteDebitCommand> {
		#region Public Constructors

		public DeleteDebitCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(DeleteDebitCommand command) {
			var obj = Repository.FindOne<Debit>(_ => _.ID == command.ID);

			if (obj == null) {
				throw new EntityNotFoundException(typeof(Debit));
			}

			Repository.Delete(obj);
		}

		#endregion
	}

	public class StoreDebitCommand : ICommand {
		#region Public Properties

		public long ID { get; set; }
		public string Description { get; set; }
		public decimal Value { get; set; }
		public DateTime Date { get; set; }
		public long FundSourceID { get; set; }
		public long CategoryID { get; set; }

		#endregion
	}

	public class StoreDebitCommandHandler : CommandHandlerBase<StoreDebitCommand> {
		#region Public Constructors

		public StoreDebitCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(StoreDebitCommand command) {
			var obj = Repository.FindOne<Debit>(_ => _.ID == command.ID)
				?? new Debit();

			obj.Description = command.Description;
			obj.Value = command.Value;
			obj.Date = command.Date;
			obj.FundSource = Repository.Load<FundSource>(command.FundSourceID);
			obj.Category = Repository.Load<Category>(command.CategoryID);

			Repository.Store(obj);

			command.ID = obj.ID;
		}

		#endregion
	}
}