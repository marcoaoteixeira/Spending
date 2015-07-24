using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using Nameless.Framework;
using Nameless.Framework.Data;
using Nameless.Spending.Core.CommandQuery.Commands;
using Nameless.Spending.Core.Models;
using NUnit.Framework;

namespace Nameless.Spending.Core.UnitTest.CommandQuery.Commands {
	[TestFixture]
	public class BudgetCommandHandlerTest {
		[Test]
		public void Can_Store_A_New_Budget() {
			// arrange
			var repository = new Mock<IRepository>();
			var command = new AlterBudgetCommand {
				CategoryID = 1,
				Description = "Test Budget",
				BudgetID = 0,
				PeriodMonth = 1,
				PeriodYear = 2015
			};
			var handler = new AlterBudgetCommandHandler(repository.Object);
			Budget stored = null;

			repository
				.Setup(_ => _.FindOne<Budget>(It.IsAny<Expression<Func<Budget, bool>>>()))
				.Returns((Budget)null)
				.Verifiable();
			repository
				.Setup(_ => _.Load<Category>(It.IsAny<long>()))
				.Returns(() => {
					var result = new Category();

					ReflectionHelper.SetPrivateFieldValue(result, "_id", command.CategoryID);

					return result;
				})
				.Verifiable();

			repository
				.Setup(_ => _.Store(It.IsAny<object>()))
				.Callback<object>(_ => {
					stored = (Budget)_;
					ReflectionHelper.SetPrivateFieldValue(stored, "_id", stored.ID + 1);
				})
				.Verifiable();

			// act
			handler.Handle(command);

			// assert
			Assert.IsNotNull(stored);
			Assert.AreNotEqual(0, stored.ID);
			Assert.AreEqual(command.CategoryID, stored.Category.ID);
			Assert.AreEqual(command.Description, stored.Description);
			Assert.AreEqual(command.PeriodMonth, stored.Period.Month);
			Assert.AreEqual(command.PeriodYear, stored.Period.Year);
			repository.VerifyAll();
		}

		[Test]
		public void Can_Update_Budget() {
			// arrange
			const string outdated = "out-dated-value";
			const string updated = "up-dated-value";
			var dataSource = new List<Budget>();
			var now = DateTime.Now.Date;

			5.Times(_ => {
				var obj = new Budget {
					Category = new Category {
						Description = outdated
					},
					Description = outdated,
					Period = new BudgetPeriod {
						Month = 1 + _,
						Year = 1 + _
					}
				};

				ReflectionHelper.SetPrivateFieldValue(obj, "_id", 1 + _);

				dataSource.Add(obj);
			});

			var repository = new Mock<IRepository>();
			var command = new AlterBudgetCommand {
				CategoryID = 1,
				Description = "Test Budget",
				BudgetID = 0,
				PeriodMonth = 1,
				PeriodYear = 2015
			};
			var handler = new AlterBudgetCommandHandler(repository.Object);
			Budget stored = null;

			repository
				.Setup(_ => _.FindOne<Budget>(It.IsAny<Expression<Func<Budget, bool>>>()))
				.Returns((Budget)null)
				.Verifiable();
			repository
				.Setup(_ => _.Load<Category>(It.IsAny<long>()))
				.Returns(() => {
					var result = new Category();

					ReflectionHelper.SetPrivateFieldValue(result, "_id", command.CategoryID);

					return result;
				})
				.Verifiable();

			repository
				.Setup(_ => _.Store(It.IsAny<object>()))
				.Callback<object>(_ => {
					stored = (Budget)_;
					ReflectionHelper.SetPrivateFieldValue(stored, "_id", stored.ID + 1);
				})
				.Verifiable();

			// act
			handler.Handle(command);

			// assert
			Assert.IsNotNull(stored);
			Assert.AreNotEqual(0, stored.ID);
			Assert.AreEqual(command.CategoryID, stored.Category.ID);
			Assert.AreEqual(command.Description, stored.Description);
			Assert.AreEqual(command.PeriodMonth, stored.Period.Month);
			Assert.AreEqual(command.PeriodYear, stored.Period.Year);
			repository.VerifyAll();
		}

		[Test]
		public void Can_Delete_Budget() {
			// arrange
			var repository = new Mock<IRepository>();
			var command = new DeleteBudgetCommand {
				BudgetID = 3,
			};
			var handler = new DeleteBudgetCommandHandler(repository.Object);
			var dataStore = new List<Budget>();

			5.Times(_ => {
				var budget = new Budget();

				ReflectionHelper.SetPrivateFieldValue(budget, "_id", _ + 1);

				dataStore.Add(budget);
			});

			repository
				.Setup(_ => _.FindOne<Budget>(It.IsAny<Expression<Func<Budget, bool>>>()))
				.Returns<Expression<Func<Budget, bool>>>(expression => dataStore.SingleOrDefault(expression.Compile()))
				.Verifiable();

			Budget deleted = null;
			repository
				.Setup(_ => _.Delete(It.IsAny<object>()))
				.Callback<object>(_ => {
					deleted = (Budget)_;
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