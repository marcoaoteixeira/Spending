using Nameless.Framework;
using Nameless.Framework.CommandQuery;
using Nameless.Framework.Data;

namespace Nameless.Spending.Core.CommandQuery.Commands {
	public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand>
		where TCommand : ICommand {
		#region Private Read-Only Fields

		private readonly IRepository _repository;

		#endregion

		#region Protected Properties

		protected IRepository Repository {
			get { return _repository; }
		}

		#endregion

		#region Protected Constructors

		protected CommandHandlerBase(IRepository repository) {
			Guard.Against.Null(repository, "repository");

			_repository = repository;
		}

		#endregion

		#region ICommandHandler<TCommand> Members

		public abstract void Handle(TCommand command);

		#endregion
	}
}