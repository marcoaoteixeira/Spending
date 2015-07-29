using Castle.DynamicProxy;
using Nameless.Framework;
using Nameless.Framework.Data;
using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Core.Infrastructure {
	public sealed class EntityWriterInterceptor : IRepositoryInterceptor {
		#region Private Read-Only Fields

		private readonly IClock _clock;

		#endregion

		#region Public Constructors

		public EntityWriterInterceptor(IClock clock) {
			Guard.Against.Null(clock, "clock");

			_clock = clock;
		}

		#endregion

		#region IRepositoryInterceptor Members

		public void Intercept(IInvocation invocation) {
			Guard.Against.Null(invocation, "invocation");

			if (invocation.Method.Name != "Store") {
				invocation.Proceed();

				return;
			}

			var repository = invocation.TargetType as IRepository;
			var entity = invocation.Arguments[0] as Entity;

			if (entity != null) {
				if (entity.ID == 0) {
					ReflectionHelper.SetPrivateFieldValue(entity, "_dateCreated", _clock.UtcNow);
				}

				ReflectionHelper.SetPrivateFieldValue(entity, "_dateModified", _clock.UtcNow);
			}

			invocation.Proceed();
		}

		#endregion
	}
}