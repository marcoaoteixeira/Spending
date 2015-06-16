using System;
using Nameless.Framework.CommandQuery;
using Nameless.Framework.Data;
using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Core.CommandQuery.Commands {
	public class DeleteCreditCommand : ICommand {
		#region Public Properties

		public long ID { get; set; }

		#endregion
	}

	public class DeleteCreditCommandHandler : CommandHandlerBase<DeleteCreditCommand> {
		#region Public Constructors

		public DeleteCreditCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(DeleteCreditCommand command) {
			var obj = Repository.FindOne<Credit>(_ => _.ID == command.ID);

			if (obj == null) {
				throw new EntityNotFoundException(typeof(Credit));
			}

			Repository.Delete(obj);
		}

		#endregion
	}

	public class StoreCreditCommand : ICommand {
		#region Public Properties

		public long ID { get; set; }
		public string Description { get; set; }
		public decimal Value { get; set; }
		public DateTime Date { get; set; }
		public long FundSourceID { get; set; }

		#endregion
	}

	public class StoreCreditCommandHandler : CommandHandlerBase<StoreCreditCommand> {
		#region Public Constructors

		public StoreCreditCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(StoreCreditCommand command) {
			var obj = Repository.FindOne<Credit>(_ => _.ID == command.ID)
				?? new Credit();

			obj.Description = command.Description;
			obj.Value = command.Value;
			obj.Date = command.Date;
			obj.FundSource = Repository.Load<FundSource>(command.FundSourceID);

			Repository.Store(obj);

			command.ID = obj.ID;
		}

		#endregion
	}
}