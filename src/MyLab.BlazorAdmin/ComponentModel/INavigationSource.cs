namespace MyLab.BlazorAdmin.ComponentModel
{
    /// <summary>
    /// Represent a navigation source
    /// </summary>
    public interface INavigationSource
    {
        /// <summary>
        /// Provides navigation links
        /// </summary>
        IEnumerable<NavigationLink> GetNavigation();
    }
}
