using System;
using Nameless.Framework.CommandQuery;
using Nameless.Framework.Data;
using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Core.CommandQuery.Commands {
	public class AlterDebitCommand : ICommand {
		#region Public Properties

		public long DebitID { get; set; }
		public string Description { get; set; }
		public decimal Value { get; set; }
		public DateTime Date { get; set; }
		public long CurrentFundSourceID { get; set; }
		public long AlterFundSourceID { get; set; }
		public long CategoryID { get; set; }

		#endregion
	}

	public class AlterDebitCommandHandler : CommandHandlerBase<AlterDebitCommand> {
		#region Public Constructors

		public AlterDebitCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(AlterDebitCommand command) {
			var obj = Repository.FindOne<Debit>(_ =>
				_.ID == command.DebitID &&
				_.FundSource.ID == command.CurrentFundSourceID);

			if (obj == null) {
				throw new EntityNotFoundException(typeof(Debit));
			}

			obj.Description = command.Description;
			obj.Value = command.Value;
			obj.Date = command.Date;

			if (command.AlterFundSourceID > 0) {
				obj.FundSource = Repository.Load<FundSource>(command.AlterFundSourceID);
			}

			obj.Category = Repository.Load<Category>(command.CategoryID);

			Repository.Store(obj);
		}

		#endregion
	}

	public class CreateDebitCommand : ICommand {
		#region Public Properties

		public long DebitID { get; set; }
		public string Description { get; set; }
		public decimal Value { get; set; }
		public DateTime Date { get; set; }
		public long FundSourceID { get; set; }
		public long CategoryID { get; set; }

		#endregion
	}

	public class CreateDebitCommandHandler : CommandHandlerBase<CreateDebitCommand> {
		#region Public Constructors

		public CreateDebitCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(CreateDebitCommand command) {
			var obj = new Debit {
				Description = command.Description,
				Value = command.Value,
				Date = command.Date,
				FundSource = Repository.Load<FundSource>(command.FundSourceID),
				Category = Repository.Load<Category>(command.CategoryID)
			};

			Repository.Store(obj);

			command.DebitID = obj.ID;
		}

		#endregion
	}

	public class DeleteDebitCommand : ICommand {
		#region Public Properties

		public long DebitID { get; set; }
		public long FundSourceID { get; set; }

		#endregion
	}

	public class DeleteDebitCommandHandler : CommandHandlerBase<DeleteDebitCommand> {
		#region Public Constructors

		public DeleteDebitCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(DeleteDebitCommand command) {
			var obj = Repository.FindOne<Debit>(_ =>
				_.ID == command.DebitID &&
				_.FundSource.ID == command.FundSourceID);

			if (obj == null) {
				throw new EntityNotFoundException(typeof(Debit));
			}

			Repository.Delete(obj);
		}

		#endregion
	}
}