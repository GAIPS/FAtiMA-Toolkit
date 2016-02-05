using System;
using System.Linq.Expressions;

namespace EmotionalAppraisalWF
{
    class HelperMethods
    {
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }
    }
}
