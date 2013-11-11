using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JLN.Controls
{
    public static class RefelectionHelper
    {
        public static string GetPropertyName(Expression<Func<object>> property)
        {
            var expression =(MemberExpression) property.Body;
            return expression.Member.Name;
        }
    }
}
