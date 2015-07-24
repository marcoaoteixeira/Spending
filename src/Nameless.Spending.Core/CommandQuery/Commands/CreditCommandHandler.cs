using System;
using Nameless.Framework.CommandQuery;
using Nameless.Framework.Data;
using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Core.CommandQuery.Commands {
	public class AlterCreditCommand : ICommand {
		#region Public Properties

		public long CreditID { get; set; }
		public string Description { get; set; }
		public decimal Value { get; set; }
		public DateTime Date { get; set; }
		public long CurrentFundSourceID { get; set; }
		public long AlterFundSourceID { get; set; }

		#endregion
	}

	public class AlterCreditCommandHandler : CommandHandlerBase<AlterCreditCommand> {
		#region Public Constructors

		public AlterCreditCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(AlterCreditCommand command) {
			var obj = Repository.FindOne<Credit>(_ =>
				_.ID == command.CreditID &&
				_.FundSource.ID == command.CurrentFundSourceID);

			if (obj == null) {
				throw new EntityNotFoundException(typeof(Credit));
			}

			obj.Description = command.Description;
			obj.Value = command.Value;
			obj.Date = command.Date;

			if (command.AlterFundSourceID > 0) {
				obj.FundSource = Repository.Load<FundSource>(command.AlterFundSourceID);
			}

			Repository.Store(obj);
		}

		#endregion
	}

	public class CreateCreditCommand : ICommand {
		#region Public Properties

		public long CreditID { get; set; }
		public string Description { get; set; }
		public decimal Value { get; set; }
		public DateTime Date { get; set; }
		public long FundSourceID { get; set; }

		#endregion
	}

	public class CreateCreditCommandHandler : CommandHandlerBase<CreateCreditCommand> {
		#region Public Constructors

		public CreateCreditCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(CreateCreditCommand command) {
			var obj = new Credit {
				Description = command.Description,
				Value = command.Value,
				Date = command.Date,
				FundSource = Repository.Load<FundSource>(command.FundSourceID)
			};

			Repository.Store(obj);

			command.CreditID = obj.ID;
		}

		#endregion
	}

	public class DeleteCreditCommand : ICommand {
		#region Public Properties

		public long FundSourceID { get; set; }
		public long CreditID { get; set; }

		#endregion
	}

	public class DeleteCreditCommandHandler : CommandHandlerBase<DeleteCreditCommand> {
		#region Public Constructors

		public DeleteCreditCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(DeleteCreditCommand command) {
			var obj = Repository.FindOne<Credit>(_ =>
				_.ID == command.CreditID &&
				_.FundSource.ID == command.FundSourceID);

			if (obj == null) {
				throw new EntityNotFoundException(typeof(Credit));
			}

			Repository.Delete(obj);
		}

		#endregion
	}
}