using Microsoft.AspNetCore.Components;

namespace MyLab.BlazorAdmin.Tools.Rendering
{
    /// <summary>
    /// Defines an object which creates a render fragment and provides template model
    /// </summary>
    public abstract class RenderFragmentFactory
    {
        /// <summary>
        /// Creates a <see cref="RenderFragment"/>
        /// </summary>
        public abstract RenderFragment CreateRenderFragment(Action<object?>? modelCallback = null);

        /// <summary>
        /// Converts <see cref="string"/> to <see cref="RenderFragmentFactory"/>
        /// </summary>
        public static implicit operator RenderFragmentFactory(string str)
        {
            return new ParagraphRenderFragmentFactory(str);
        }
    }
}
