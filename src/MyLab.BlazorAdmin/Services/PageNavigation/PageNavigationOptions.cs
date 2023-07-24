using MyLab.BlazorAdmin.ComponentModel;

namespace MyLab.BlazorAdmin.Services.PageNavigation
{
    /// <summary>
    /// Describes the page navigation
    /// </summary>
    public class PageNavigationOptions
    {
        /// <summary>
        /// Gets or sets Navigation category definitions
        /// </summary>
        public NavigationCategoryDefinition[] Navigation { get; set; }

        /// <summary>
        /// Gets or sets fasc access links
        /// </summary>
        public NavigationLink[] FastLinks { get; set; }
    }
}
