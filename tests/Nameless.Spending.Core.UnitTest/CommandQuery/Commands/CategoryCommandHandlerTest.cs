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
	public class CategoryCommandHandlerTest {
		[Test]
		public void Can_Store_A_New_Category() {
			// arrange
			var repository = new Mock<IRepository>();
			var command = new CreateCategoryCommand {
				Description = "Test Category",
				ID = 0,
				Ancestors = new[] { 3L }
			};
			var handler = new CreateCategoryCommandHandler(repository.Object);
			Category stored = null;
			var dataStore = new List<Category>();

			5.Times(_ => {
				var category = new Category();

				ReflectionHelper.SetPrivateFieldValue(category, "_id", _ + 1);

				dataStore.Add(category);
			});

			repository
				.Setup(_ => _.FindOne<Category>(It.IsAny<Expression<Func<Category, bool>>>()))
				.Returns((Category)null)
				.Verifiable();

			repository
				.Setup(_ => _.Load<Category>(It.IsAny<long>()))
				.Returns<long>(_ => dataStore.Single(item => item.ID == _))
				.Verifiable();

			repository
				.Setup(_ => _.Store(It.IsAny<object>()))
				.Callback<object>(_ => {
					stored = (Category)_;
					ReflectionHelper.SetPrivateFieldValue(stored, "_id", stored.ID + 1);
				})
				.Verifiable();

			// act
			handler.Handle(command);

			// assert
			Assert.IsNotNull(stored);
			Assert.AreNotEqual(0, stored.ID);
			Assert.AreEqual(command.Description, stored.Description);
			CollectionAssert.IsNotEmpty(stored.Ancestors);
			Assert.AreEqual(command.Ancestors.Single(), stored.Ancestors.Single().ID);
			repository.VerifyAll();
		}

		[Test]
		public void Can_Delete_Category() {
			// arrange
			var repository = new Mock<IRepository>();
			var command = new DeleteCategoryCommand {
				CategoryID = 3,
			};
			var handler = new DeleteCategoryCommandHandler(repository.Object);
			var dataStore = new List<Category>();

			5.Times(_ => {
				var category = new Category();

				ReflectionHelper.SetPrivateFieldValue(category, "_id", _ + 1);

				dataStore.Add(category);
			});

			repository
				.Setup(_ => _.FindOne<Category>(It.IsAny<Expression<Func<Category, bool>>>()))
				.Returns<Expression<Func<Category, bool>>>(expression => dataStore.SingleOrDefault(expression.Compile()))
				.Verifiable();

			Category deleted = null;
			repository
				.Setup(_ => _.Delete(It.IsAny<object>()))
				.Callback<object>(_ => {
					deleted = (Category)_;
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