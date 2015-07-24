using Nameless.Framework.CommandQuery;
using Nameless.Framework.Data;
using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Core.CommandQuery.Commands {
	public class AlterFundSourceCommand : ICommand {
		#region Public Properties

		public long FundSourceID { get; set; }
		public string Name { get; set; }

		#endregion
	}

	public class AlterFundSourceCommandHandler : CommandHandlerBase<AlterFundSourceCommand> {
		#region Public Constructors

		public AlterFundSourceCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(AlterFundSourceCommand command) {
			var obj = Repository.FindOne<FundSource>(_ => _.ID == command.FundSourceID);

			if (obj == null) {
				throw new EntityNotFoundException(typeof(FundSource));
			}

			obj.Name = command.Name;

			Repository.Store(obj);
		}

		#endregion
	}

	public class CreateFundSourceCommand : ICommand {
		#region Public Properties

		public long FundSourceID { get; set; }
		public string Name { get; set; }

		#endregion
	}

	public class CreateFundSourceCommandHandler : CommandHandlerBase<CreateFundSourceCommand> {
		#region Public Constructors

		public CreateFundSourceCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(CreateFundSourceCommand command) {
			var obj = new FundSource {
				Name = command.Name
			};

			Repository.Store(obj);

			command.FundSourceID = obj.ID;
		}

		#endregion
	}

	public class DeleteFundSourceCommand : ICommand {
		#region Public Properties

		public long ID { get; set; }

		#endregion
	}

	public class DeleteFundSourceCommandHandler : CommandHandlerBase<DeleteFundSourceCommand> {
		#region Public Constructors

		public DeleteFundSourceCommandHandler(IRepository repository)
			: base(repository) { }

		#endregion

		#region Public Override Methods

		public override void Handle(DeleteFundSourceCommand command) {
			var fundSource = Repository.FindOne<FundSource>(_ =>
				_.ID == command.ID);

			if (fundSource == null) {
				throw new EntityNotFoundException(typeof(FundSource));
			}

			Repository.Delete(fundSource);
		}

		#endregion
	}
}