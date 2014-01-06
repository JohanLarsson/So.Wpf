using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JLN.Controls
{
    public static class ReflectionHelper
    {
        public static string GetPropertyName(Expression<Func<object>> property)
        {
            Expression expression = property.Body;
            var unaryExpression = expression as UnaryExpression;
            if (unaryExpression != null)
            {
                Expression operand = unaryExpression.Operand;
                var memberExpression = operand as MemberExpression;
                if (memberExpression != null)
                    return memberExpression.Member.Name;
            }
            var memberExpression1 = expression as MemberExpression;
            if (memberExpression1 != null)
                return memberExpression1.Member.Name;
            throw new ArgumentException();
        }
    }
}
