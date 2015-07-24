using System;
using System.Collections.Generic;
using Nameless.Framework;
using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Core.UnitTest.Fixtures {
	public class DataSource {
		private const int ITEM_COUNT = 10;

		public List<Entity> Entities { get; set; }

		public List<Budget> Budgets { get; set; }
		public List<Category> Categories { get; set; }
		public List<Credit> Credits { get; set; }
		public List<Debit> Debits { get; set; }
		public List<FundSource> FundSources { get; set; }

		public DataSource() {
			Entities = new List<Entity>();

			Budgets = new List<Budget>();
			Categories = new List<Category>();
			Credits = new List<Credit>();
			Debits = new List<Debit>();
			FundSources = new List<FundSource>();
		}

		public static DataSource Create() {
			var result = new DataSource();

			#region Creating object instances

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

					ReflectionHelper.SetPrivateFieldValue(itemObj, "_id", itemIdx + 1);
					ReflectionHelper.SetPrivateFieldValue(itemObj, "_dateCreated", DateTime.Now);
					ReflectionHelper.SetPrivateFieldValue(itemObj, "_dateModified", DateTime.Now.AddDays(1));

					obj.Items.Add(itemObj);
				});

				ReflectionHelper.SetPrivateFieldValue(obj, "_id", idx + 1);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateCreated", DateTime.Now);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateModified", DateTime.Now.AddDays(1));
				result.Budgets.Add(obj);
			});

			ITEM_COUNT.Times(idx => {
				var obj = new Category {
					Description = string.Format("Category #{0}", idx + 1)
				};

				ReflectionHelper.SetPrivateFieldValue(obj, "_id", idx + 1);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateCreated", DateTime.Now);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateModified", DateTime.Now.AddDays(1));
				result.Categories.Add(obj);
			});

			ITEM_COUNT.Times(itemIdx => {
				var obj = new Credit {
					Description = string.Format("Credit #{0}", itemIdx + 1),
					Date = DateTime.Now,
					Value = ((itemIdx + 1) * 200)
				};

				ReflectionHelper.SetPrivateFieldValue(obj, "_id", itemIdx + 1);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateCreated", DateTime.Now);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateModified", DateTime.Now.AddDays(1));
				result.Credits.Add(obj);
			});

			ITEM_COUNT.Times(idx => {
				var obj = new Debit {
					Description = string.Format("Debit #{0}", idx + 1),
					Date = DateTime.Now,
					Value = ((idx + 1) * 125)
				};

				ReflectionHelper.SetPrivateFieldValue(obj, "_id", idx + 1);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateCreated", DateTime.Now);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateModified", DateTime.Now.AddDays(1));
				result.Debits.Add(obj);
			});

			ITEM_COUNT.Times(idx => {
				var obj = new FundSource {
					Name = string.Format("FundSource #{0}", idx + 1)
				};
				ReflectionHelper.SetPrivateFieldValue(obj, "_id", idx + 1);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateCreated", DateTime.Now);
				ReflectionHelper.SetPrivateFieldValue(obj, "_dateModified", DateTime.Now.AddDays(1));
				result.FundSources.Add(obj);
			});

			#endregion



			return result;
		}
	}
}