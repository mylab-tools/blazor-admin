using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace MyLab.BlazorAdmin.Tools.Rendering;

/// <summary>
/// Render fragment factory based on template type
/// </summary>
public class TemplateRenderFragmentFactory : RenderFragmentFactory
{
    /// <summary>
    /// Initializes a new instance of <see cref="TemplateRenderFragmentFactory"/>
    /// </summary>
    TemplateRenderFragmentFactory(Type type, IReadOnlyDictionary<string, object>? parameters)
    {
        Type = type;
        Parameters = parameters;
    }

    /// <summary>
    /// Template type
    /// </summary>
    public Type Type { get; }
    /// <summary>
    /// Parameters
    /// </summary>
    public IReadOnlyDictionary<string, object>? Parameters { get; }

    /// <summary>
    /// Creates a <see cref="RenderFragment"/>
    /// </summary>
    public override RenderFragment CreateRenderFragment(Action<object?>? modelCallback = null)
    {
        return b =>
        {
            b.OpenComponent(0, Type);

            if (Parameters != null)
            {
                foreach (var parameter in Parameters)
                {
                    b.AddAttribute(1, parameter.Key, parameter.Value);
                }
            }

            b.AddComponentReferenceCapture(2, component =>
            {
                modelCallback?.Invoke(component);
            });

            b.CloseComponent();
        };
    }

    /// <summary>
    /// Creates factory with lambda parameters definition
    /// </summary>
    public static TemplateRenderFragmentFactory Create<TTemplate>(Expression<Func<TTemplate>>? setParams = null)
    {
        return new TemplateRenderFragmentFactory(
            typeof(TTemplate),
            setParams != null
                ? InitParametersDictionary.FromExpression(setParams)
                : null
        );
    }
}