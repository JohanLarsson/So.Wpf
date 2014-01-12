namespace So.Wpf.Tests.Prototypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Windows.Data;

    public class RootedPropertyPath<T>
    {
        public Object Target { get; set; }
        public String Path { get; set; }
        public static implicit operator System.Windows.PropertyPath(RootedPropertyPath<T> self)
        {
            return new System.Windows.PropertyPath(self.Path);
        }

        public static implicit operator String(RootedPropertyPath<T> self)
        {
            return self.Path;
        }
        public Binding ToBinding(BindingMode mode = BindingMode.TwoWay)
        {
            return new Binding(Path) { Source = Target, Mode = mode };
        }
    }

    public static class RootedPropertyPath
    {
        public static RootedPropertyPath<T> Create<T>(Expression<Func<T>> expr)
        {
            Expression currentExpression = expr.Body;

            List<String> lst = new List<String>();

            ConstantExpression ce;

            while (true)
            {
                ce = currentExpression as ConstantExpression;

                var me = currentExpression as MemberExpression;

                if (ce != null) break;

                if (me == null)
                    throw new Exception(String.Format(
                        "Unexpected expression type {0} in lambda.", expr.GetType()));

                lst.Add(me.Member.Name);

                currentExpression = me.Expression;
            }

            lst.Reverse();

            return new RootedPropertyPath<T>() { Path = String.Join(".", lst), Target = ce.Value };
        }
    }
}