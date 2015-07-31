using System;
using Nameless.Spending.Core.Data.NHibernate;
using Nameless.Spending.Core.Models;
using NUnit.Framework;

namespace Nameless.Spending.Core.UnitTest.Data.NHiberate {
	[TestFixture]
	public class ModelInspectorTest {
		[Test]
		[TestCase(typeof(Budget))]
		[TestCase(typeof(BudgetItem))]
		[TestCase(typeof(Category))]
		[TestCase(typeof(Credit))]
		[TestCase(typeof(Debit))]
		[TestCase(typeof(Operation))]
		public void Returns_True_If_Type_BaseType_Is_Entity_Class(Type type) {
			// arrange
			var inspector = new ModelInspector();

			// act
			var result = inspector.IsEntity(type);

			// assert
			Assert.IsTrue(result, "Type {0} not defined as Entity.", type);
		}
	}
}