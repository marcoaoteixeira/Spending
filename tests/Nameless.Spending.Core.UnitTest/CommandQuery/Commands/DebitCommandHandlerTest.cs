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
	public class DebitCommandHandlerTest {
		[Test]
		public void Can_Store_A_New_Debit() {
			// arrange
			var repository = new Mock<IRepository>();
			var command = new CreateDebitCommand {
				Date = DateTime.Now,
				Description = "Test Debit",
				FundSourceID = 2,
				DebitID = 0,
				Value = 10m,
				CategoryID = 4
			};
			var handler = new CreateDebitCommandHandler(repository.Object);
			Debit stored = null;
			var fundSourceDataStore = new List<FundSource>();

			5.Times(_ => {
				var obj = new FundSource();

				ReflectionHelper.SetPrivateFieldValue(obj, "_id", _ + 1);

				fundSourceDataStore.Add(obj);
			});

			var categoryDataStore = new List<Category>();

			5.Times(_ => {
				var obj = new Category();

				ReflectionHelper.SetPrivateFieldValue(obj, "_id", _ + 1);

				categoryDataStore.Add(obj);
			});

			repository
				.Setup(_ => _.Load<Category>(It.IsAny<long>()))
				.Returns<long>(_ => categoryDataStore.Single(item => item.ID == _))
				.Verifiable();

			repository
				.Setup(_ => _.Load<FundSource>(It.IsAny<long>()))
				.Returns<long>(_ => fundSourceDataStore.Single(item => item.ID == _))
				.Verifiable();

			repository
				.Setup(_ => _.Store(It.IsAny<object>()))
				.Callback<object>(_ => {
					stored = (Debit)_;
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
			Assert.IsNotNull(stored.Category);
			Assert.AreEqual(command.CategoryID, stored.Category.ID);
			Assert.AreEqual(command.Value, stored.Value);
			repository.VerifyAll();
		}

		[Test]
		public void Can_Update_Debit() {
			// arrange
			var repository = new FakeRepository();
			var command = new AlterDebitCommand {
				Date = DateTime.Now,
				Description = "Test Debit",
				AlterFundSourceID = 5,
				CurrentFundSourceID = 1,
				DebitID = 1,
				Value = 10m,
				CategoryID = 4
			};
			var handler = new AlterDebitCommandHandler(repository);

			// act
			handler.Handle(command);

			var debit = repository.Load<Debit>(1);

			// assert
			Assert.IsNotNull(debit);
			Assert.AreEqual(command.Date, debit.Date);
			Assert.AreEqual(command.Description, debit.Description);
			Assert.IsNotNull(debit.FundSource);
			Assert.AreEqual(command.AlterFundSourceID, debit.FundSource.ID);
			Assert.AreEqual(command.DebitID, debit.ID);
			Assert.AreEqual(command.Value, debit.Value);
			Assert.IsNotNull(debit.Category);
			Assert.AreEqual(command.CategoryID, debit.Category.ID);
		}

		[Test]
		public void Can_Delete_Debit() {
			// arrange
			var repository = new Mock<IRepository>();
			var command = new DeleteDebitCommand {
				FundSourceID = 3,
				DebitID = 3,
			};
			var handler = new DeleteDebitCommandHandler(repository.Object);
			var dataStore = new List<Debit>();

			5.Times(_ => {
				var debit = new Debit {
					FundSource = new FundSource()
				};

				ReflectionHelper.SetPrivateFieldValue(debit, "_id", _ + 1);
				ReflectionHelper.SetPrivateFieldValue(debit.FundSource, "_id", _ + 1);

				dataStore.Add(debit);
			});

			repository
				.Setup(_ => _.FindOne<Debit>(It.IsAny<Expression<Func<Debit, bool>>>()))
				.Returns<Expression<Func<Debit, bool>>>(expression => dataStore.SingleOrDefault(expression.Compile()))
				.Verifiable();

			Debit deleted = null;
			repository
				.Setup(_ => _.Delete(It.IsAny<object>()))
				.Callback<object>(_ => {
					deleted = (Debit)_;
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