using System;
using System.Linq;
using System.Collections.Generic;
using Nameless.Framework;
using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Core.UnitTest.Fixtures {
	public class DataSource {
		private const int ITEM_COUNT = 10;

		public List<Entity> Entities { get; set; }

		private IEnumerable<Budget> Budgets {
			get { return Entities.OfType<Budget>(); }
		}
		private IEnumerable<Category> Categories {
			get { return Entities.OfType<Category>(); }
		}
		private IEnumerable<Credit> Credits {
			get { return Entities.OfType<Credit>(); }
		}
		private IEnumerable<Debit> Debits {
			get { return Entities.OfType<Debit>(); }
		}
		private IEnumerable<FundSource> FundSources {
			get { return Entities.OfType<FundSource>(); }
		}

		public DataSource() {
			Entities = new List<Entity>();
		}

		public static DataSource Create() {
			var result = new DataSource();

			#region Creating object instances

			#region Category

			ITEM_COUNT.Times(idx => {
				var obj = new Category {
					Description = string.Format("Category #{0}", idx + 1)
				};

				ReflectionHelper.SetPrivateFieldValue(obj, "_id", idx + 1);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateCreated", DateTime.Now);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateModified", DateTime.Now.AddDays(1));
				result.Entities.Add(obj);
			});

			#endregion

			#region Budget And BudgetItem

			ITEM_COUNT.Times(idx => {
				var obj = new Budget {
					Description = string.Format("Budget #{0}", idx + 1),
					Period = new BudgetPeriod {
						Month = idx + 1,
						Year = idx + 1
					}
				};

				ITEM_COUNT.Times(itemIdx => {
					var itemObj = new BudgetItem {
						Budget = obj,
						Description = string.Format("BudgetItem #{0} for Budget {1}", itemIdx + 1, obj.Description),
						Value = ((itemIdx + 1) * idx) + 175
					};

					obj.Category = result.Categories.ElementAt(itemIdx);

					ReflectionHelper.SetPrivateFieldValue(itemObj, "_id", itemIdx + 1);
					ReflectionHelper.SetPrivateFieldValue(itemObj, "_dateCreated", DateTime.Now);
					ReflectionHelper.SetPrivateFieldValue(itemObj, "_dateModified", DateTime.Now.AddDays(1));

					obj.Items.Add(itemObj);
				});

				ReflectionHelper.SetPrivateFieldValue(obj, "_id", idx + 1);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateCreated", DateTime.Now);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateModified", DateTime.Now.AddDays(1));
				result.Entities.Add(obj);
			});

			#endregion

			#region Credit

			ITEM_COUNT.Times(itemIdx => {
				var obj = new Credit {
					Description = string.Format("Credit #{0}", itemIdx + 1),
					Date = DateTime.Now,
					Value = ((itemIdx + 1) * 200)
				};

				ReflectionHelper.SetPrivateFieldValue(obj, "_id", itemIdx + 1);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateCreated", DateTime.Now);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateModified", DateTime.Now.AddDays(1));
				result.Entities.Add(obj);
			});

			#endregion

			#region Debit

			ITEM_COUNT.Times(idx => {
				var obj = new Debit {
					Description = string.Format("Debit #{0}", idx + 1),
					Date = DateTime.Now,
					Value = ((idx + 1) * 125)
				};

				obj.Category = result.Categories.ElementAt(idx);

				ReflectionHelper.SetPrivateFieldValue(obj, "_id", idx + 1);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateCreated", DateTime.Now);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateModified", DateTime.Now.AddDays(1));
				result.Entities.Add(obj);
			});

			#endregion

			#region FundSource

			ITEM_COUNT.Times(idx => {
				var obj = new FundSource {
					Name = string.Format("FundSource #{0}", idx + 1)
				};

				ITEM_COUNT.Times(creditIdx => obj.Credits.Add(result.Credits.ElementAt(creditIdx)));
				ITEM_COUNT.Times(debitIdx => obj.Debits.Add(result.Debits.ElementAt(debitIdx)));

				ReflectionHelper.SetPrivateFieldValue(obj, "_id", idx + 1);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateCreated", DateTime.Now);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateModified", DateTime.Now.AddDays(1));
				ReflectionHelper.SetPrivateFieldValue(obj, "_totalCredit", result.Credits.Sum(_ => _.Value));
				ReflectionHelper.SetPrivateFieldValue(obj, "_totalDebit", result.Debits.Sum(_ => _.Value));

				result.Entities.Add(obj);
			});

			result.Entities.OfType<Credit>().Each((_, idx) => {
				_.FundSource = result.Entities.OfType<FundSource>().ElementAt(idx);
			});

			result.Entities.OfType<Debit>().Each((_, idx) => {
				_.FundSource = result.Entities.OfType<FundSource>().ElementAt(idx);
			});

			#endregion

			#endregion

			return result;
		}
	}
}