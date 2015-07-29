using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using Nameless.Framework;
using Nameless.Framework.Data;
using Nameless.Spending.Core.CommandQuery.Commands;
using Nameless.Spending.Core.Models;
using Nameless.Spending.Core.UnitTest.Fixtures;
using NUnit.Framework;

namespace Nameless.Spending.Core.UnitTest.CommandQuery.Commands {
	[TestFixture]
	public class FundSourceCommandHandlerTest {
		[Test]
		public void Can_Store_A_New_FundSource() {
			// arrange
			var repository = new Mock<IRepository>();
			var command = new CreateFundSourceCommand {
				Name = "Test FundSource",
				FundSourceID = 0
			};
			var handler = new CreateFundSourceCommandHandler(repository.Object);
			FundSource stored = null;
			var dataStore = new List<FundSource>();

			5.Times(_ => {
				var fundSource = new FundSource();

				ReflectionHelper.SetPrivateFieldValue(fundSource, "_id", _ + 1);

				dataStore.Add(fundSource);
			});

			repository
				.Setup(_ => _.Store(It.IsAny<object>()))
				.Callback<object>(_ => {
					stored = (FundSource)_;
					ReflectionHelper.SetPrivateFieldValue(stored, "_id", stored.ID + 1);
				})
				.Verifiable();

			// act
			handler.Handle(command);

			// assert
			Assert.IsNotNull(stored);
			Assert.AreNotEqual(0, stored.ID);
			Assert.AreEqual(command.Name, stored.Name);
			repository.VerifyAll();
		}

		[Test]
		public void Can_Update_FundSource() {
			// arrange
			var repository = new FakeRepository();
			var command = new AlterFundSourceCommand {
				Name = "Test FundSource",
				FundSourceID = 1
			};
			var handler = new AlterFundSourceCommandHandler(repository);

			// act
			handler.Handle(command);

			var fundSource = repository.Load<FundSource>(1);

			// assert
			Assert.IsNotNull(fundSource);
			Assert.AreEqual(command.Name, fundSource.Name);
			Assert.AreEqual(command.FundSourceID, fundSource.ID);
		}

		[Test]
		public void Can_Delete_FundSource() {
			// arrange
			var repository = new Mock<IRepository>();
			var command = new DeleteFundSourceCommand {
				ID = 3,
			};
			var handler = new DeleteFundSourceCommandHandler(repository.Object);
			var dataStore = new List<FundSource>();

			5.Times(_ => {
				var fundSource = new FundSource();

				ReflectionHelper.SetPrivateFieldValue(fundSource, "_id", _ + 1);

				dataStore.Add(fundSource);
			});

			repository
				.Setup(_ => _.FindOne<FundSource>(It.IsAny<Expression<Func<FundSource, bool>>>()))
				.Returns<Expression<Func<FundSource, bool>>>(expression => dataStore.SingleOrDefault(expression.Compile()))
				.Verifiable();

			FundSource deleted = null;
			repository
				.Setup(_ => _.Delete(It.IsAny<object>()))
				.Callback<object>(_ => {
					deleted = (FundSource)_;
					dataStore.Remove(deleted);
				})
				.Verifiable();

			// act
			handler.Handle(command);

			// assert
			Assert.AreEqual(4, dataStore.Count);
			CollectionAssert.DoesNotContain(dataStore, deleted);
			repository.VerifyAll();
		}
	}
}