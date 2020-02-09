using System;
using System.Linq.Expressions;

namespace DEDrake.Services.Utils {
  internal class ExpressionTransform<TInterface, TConcrete> : ExpressionVisitor {
    private readonly ParameterExpression _param = Expression.Parameter(typeof(TConcrete), "param_0");

    public static Expression<Func<TConcrete, bool>> Transform(Expression exp) {
      var et = new ExpressionTransform<TInterface, TConcrete>();
      return (Expression<Func<TConcrete, bool>>)et.Visit(exp);
    }

    protected override Expression VisitLambda<T>(Expression<T> exp) {
      if (typeof(T).IsAssignableFrom(typeof(Func<TInterface, bool>))) {
        return Expression.Lambda<Func<TConcrete, bool>>(Visit(exp.Body), _param);
      }

      return base.VisitLambda(exp);
    }

    protected override Expression VisitMember(MemberExpression exp) {
      if (exp.Member.DeclaringType.IsAssignableFrom(typeof(TInterface))) {
        return Expression.MakeMemberAccess(Visit(exp.Expression), typeof(TConcrete).GetProperty(exp.Member.Name));
      }

      return base.VisitMember(exp);
    }

    protected override Expression VisitParameter(ParameterExpression exp) {
      if (exp.Type.IsAssignableFrom(typeof(TInterface))) { return _param; }

      return base.VisitParameter(exp);
    }
  }
}
