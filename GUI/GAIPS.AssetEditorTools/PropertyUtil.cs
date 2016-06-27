using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace GAIPS.AssetEditorTools
{
	public static class PropertyUtil
	{
		public static string GetPropertyName<TObject>(Expression<Func<TObject, object>> propertyRefExpr)
		{
			return GetPropertyNameCore(propertyRefExpr.Body);
		}

		public static PropertyDescriptor GetPropertyDescriptor<TObject>(Expression<Func<TObject, object>> propertyRefExpr)
		{
			return GetPropertyDescriptor<TObject>(GetPropertyNameCore(propertyRefExpr.Body), false);
		}

		public static PropertyDescriptor GetPropertyDescriptor<TObject>(string propertyName, bool ignoreCase = false)
		{
			return TypeDescriptor.GetProperties(typeof (TObject)).Find(propertyName, ignoreCase);
		}

		private static string GetPropertyNameCore(Expression propertyRefExpr)
		{
			if (propertyRefExpr == null)
				throw new ArgumentNullException(nameof(propertyRefExpr));

			var memberExpr = propertyRefExpr as MemberExpression;
			if (memberExpr == null)
			{
				var unaryExpr = propertyRefExpr as UnaryExpression;
				if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
					memberExpr = unaryExpr.Operand as MemberExpression;
			}

			if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
				return memberExpr.Member.Name;

			throw new ArgumentException("No property reference expression was found.", nameof(propertyRefExpr));
		}
	}
}