using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using MyLab.ExpressionTools;

namespace MyLab.BlazorAdmin.Tools;

/// <summary>
/// Contains parameters for component initialization
/// </summary>
public class InitParametersDictionary : ReadOnlyDictionary<string, object>
{
    /// <summary>
    /// Initializes a new instance of <see cref="InitParametersDictionary"/>
    /// </summary>
    public InitParametersDictionary(IDictionary<string, object> init) : base(init)
    {
        
    }

    /// <summary>
    /// Creates <see cref="InitParametersDictionary"/> from member init expression
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <param name="expression">Member init expression</param>
    public static InitParametersDictionary FromExpression<T>(Expression<Func<T>> expression)
    {
        if (expression == null) throw new ArgumentNullException(nameof(expression));

        if (expression.Body.NodeType != ExpressionType.MemberInit)
        {
            throw new NotSupportedException("Only member init expressions are supported");
        }

        var memberInitExpr = (MemberInitExpression)expression.Body;

        if (memberInitExpr.NewExpression.Arguments.Count > 0)
        {
            throw new NotSupportedException("Only default constructor must be used");
        }

        if (memberInitExpr.Bindings.Any(b => b.BindingType != MemberBindingType.Assignment))
        {
            throw new NotSupportedException("All parameter bindings must be an assignment");
        }

        if (memberInitExpr.Bindings.Any(b => b.Member.GetCustomAttribute<ParameterAttribute>() == null))
        {
            throw new NotSupportedException("All parameter must be marked by " + nameof(ParameterAttribute));
        }

        var dict = memberInitExpr.Bindings
            .Cast<MemberAssignment>()
            .ToDictionary(
                b => b.Member.Name, 
                b => b.Expression.GetValue<object>());

        return new InitParametersDictionary(dict);
    }
}