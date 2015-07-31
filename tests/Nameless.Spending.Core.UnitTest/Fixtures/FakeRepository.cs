using System;
using System.Linq;
using Nameless.Framework.Data;
using Nameless.Spending.Core.Models;

namespace Nameless.Spending.Core.UnitTest.Fixtures {
	public class FakeRepository : IRepository {
		private DataSource _dataSource;

		public FakeRepository(DataSource dataSource = null) {
			_dataSource = dataSource ?? DataSource.Create();
		}
		
		#region IRepository Members

		public void Store(object obj) {
			var items = _dataSource.Entities.Where(_ => _.GetType() == obj.GetType()).ToList();
			var selected = items.SingleOrDefault(_ => ((Entity)_).ID == ((Entity)obj).ID);
			var actual = -1;

			if (selected != null) {
				actual = items.IndexOf(selected);
			}

			if (actual != -1) {
				items[actual] = (Entity)obj;
			} else {
				_dataSource.Entities.Add((Entity)obj);
			}
		}

		public void Delete(object obj) {
			_dataSource.Entities.Remove((Entity)obj);
		}

		public T Load<T>(object id) where T : class {
			var items = _dataSource.Entities.Where(_ => _.GetType() == typeof(T)).ToList();
			var selected = items.SingleOrDefault(_ => object.Equals(_.ID, Convert.ToInt64(id))) as object;

			return (T)selected;
		}

		public T FindOne<T>(System.Linq.Expressions.Expression<Func<T, bool>> filter) where T : class {
			return _dataSource.Entities.OfType<T>().AsQueryable<T>().SingleOrDefault(filter);
		}

		public IQueryable<T> Query<T>() where T : class {
			return _dataSource.Entities.OfType<T>().AsQueryable<T>();
		}

		#endregion

		#region IRepository Members


		public dynamic ExecuteDirective(IDirective directive) {
			throw new NotImplementedException();
		}

		#endregion
	}
}
