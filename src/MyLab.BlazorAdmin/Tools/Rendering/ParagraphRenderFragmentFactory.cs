using Microsoft.AspNetCore.Components;

namespace MyLab.BlazorAdmin.Tools.Rendering;

/// <summary>
/// Render fragment factory based on string paragraph
/// </summary>
public class ParagraphRenderFragmentFactory : RenderFragmentFactory
{
    private readonly string _str;

    /// <summary>
    /// Initializes a new instance of <see cref="ParagraphRenderFragmentFactory"/>
    /// </summary>
    public ParagraphRenderFragmentFactory(string str)
    {
        _str = str;
    }

    /// <inheritdoc />
    public override RenderFragment CreateRenderFragment(Action<object?>? modelCallback = null)
    {
        return b =>
        {
            int i = 0;
            b.OpenElement(i++, "p");
            b.AddAttribute(i++, "class", "mb-0");
            b.AddContent(i++, _str);
            b.CloseElement();
        };
    }
}