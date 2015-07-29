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
	public class CreditCommandHandlerTest {
		[Test]
		public void Can_Store_A_New_Credit() {
			// arrange
			var repository = new Mock<IRepository>();
			var command = new CreateCreditCommand {
				Date = DateTime.Now,
				Description = "Test Credit",
				FundSourceID = 2,
				CreditID = 0,
				Value = 10m
			};
			var handler = new CreateCreditCommandHandler(repository.Object);
			Credit stored = null;
			var dataStore = new List<FundSource>();

			5.Times(_ => {
				var obj = new FundSource();

				ReflectionHelper.SetPrivateFieldValue(obj, "_id", _ + 1);

				dataStore.Add(obj);
			});

			repository
				.Setup(_ => _.Load<FundSource>(It.IsAny<long>()))
				.Returns<long>(_ => dataStore.Single(item => item.ID == _))
				.Verifiable();

			repository
				.Setup(_ => _.Store(It.IsAny<object>()))
				.Callback<object>(_ => {
					stored = (Credit)_;
					ReflectionHelper.SetPrivateFieldValue(stored, "_id", stored.ID + 1);
				})
				.Verifiable();

			// act
			handler.Handle(command);

			// assert
			Assert.IsNotNull(stored);
			Assert.AreNotEqual(0, stored.ID);
			Assert.AreEqual(command.Date, stored.Date);
			Assert.AreEqual(command.Description, stored.Description);
			Assert.IsNotNull(stored.FundSource);
			Assert.AreEqual(command.FundSourceID, stored.FundSource.ID);
			Assert.AreEqual(command.Value, stored.Value);
			repository.VerifyAll();
		}

		[Test]
		public void Can_Update_Credit() {
			// arrange
			var repository = new FakeRepository();
			var command = new AlterCreditCommand {
				Date = DateTime.Now,
				Description = "Test Credit",
				CurrentFundSourceID = 1,
				AlterFundSourceID = 3,
				CreditID = 1,
				Value = 10m
			};
			var handler = new AlterCreditCommandHandler(repository);

			// act
			handler.Handle(command);

			var credit = repository.Load<Credit>(1);

			// assert
			Assert.IsNotNull(credit);
			Assert.AreEqual(command.Date, credit.Date);
			Assert.AreEqual(command.Description, credit.Description);
			Assert.IsNotNull(credit.FundSource);
			Assert.AreEqual(command.AlterFundSourceID, credit.FundSource.ID);
			Assert.AreEqual(command.Value, credit.Value);
			Assert.AreEqual(command.CreditID, credit.ID);
		}

		[Test]
		public void Can_Delete_Credit() {
			// arrange
			var repository = new Mock<IRepository>();
			var command = new DeleteCreditCommand {
				FundSourceID = 3,
				CreditID = 3,
			};
			var handler = new DeleteCreditCommandHandler(repository.Object);
			var dataStore = new List<Credit>();

			5.Times(_ => {
				var credit = new Credit {
					FundSource = new FundSource()
				};

				ReflectionHelper.SetPrivateFieldValue(credit, "_id", _ + 1);
				ReflectionHelper.SetPrivateFieldValue(credit.FundSource, "_id", _ + 1);

				dataStore.Add(credit);
			});

			repository
				.Setup(_ => _.FindOne<Credit>(It.IsAny<Expression<Func<Credit, bool>>>()))
				.Returns<Expression<Func<Credit, bool>>>(expression => dataStore.SingleOrDefault(expression.Compile()))
				.Verifiable();

			Credit deleted = null;
			repository
				.Setup(_ => _.Delete(It.IsAny<object>()))
				.Callback<object>(_ => {
					deleted = (Credit)_;
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