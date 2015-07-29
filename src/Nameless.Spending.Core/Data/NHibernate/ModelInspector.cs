using System;
using System.Reflection;
using Nameless.Spending.Core.Models;
using NHibernate.Mapping.ByCode;

namespace Nameless.Spending.Core.Data.NHibernate {
	public class ModelInspector : ExplicitlyDeclaredModel {
		#region Public Override Methods

		public override bool IsEntity(Type type) {
			return typeof(Entity) == type.BaseType ||
				   typeof(Operation) == type.BaseType;
		}

		public override bool IsPersistentProperty(MemberInfo member) {
			if (member.DeclaringType == typeof(FundSource) && member.Name.Equals("Balance")) {
				return false;
			}

			return base.IsPersistentProperty(member);
		}

		#endregion
	}
}