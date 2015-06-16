using System;
using Nameless.Spending.Core.Models;
using NHibernate.Mapping.ByCode;

namespace Nameless.Spending.Core.Data.NHibernate {
	public class ModelInspector : ExplicitlyDeclaredModel {
		#region Public Override Methods

		public override bool IsEntity(Type type) {
			return typeof(Entity) == type.BaseType ||
				   typeof(Operation) == type.BaseType;
		}

		#endregion
	}
}