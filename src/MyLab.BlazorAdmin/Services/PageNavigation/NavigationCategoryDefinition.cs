namespace MyLab.BlazorAdmin.Services.PageNavigation
{
    /// <summary>
    /// Defines navigation category 
    /// </summary>
    public class NavigationCategoryDefinition
    {
        /// <summary>
        /// Group title
        /// </summary>
        public string? Title { get; init; }
        /// <summary>
        /// Nodes
        /// </summary>
        public PageNavigationNodeDefinition[]? Nodes { get; set; }
    }
}
