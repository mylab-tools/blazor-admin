namespace MyLab.BlazorAdmin.Shared.Search
{
    /// <summary>
    /// Describes a searcher filter
    /// </summary>
    public class FilterDesc
    {
        /// <summary>
        /// Filter identifier or null of default
        /// </summary>
        public string? Id { get; init; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; init; }

        /// <summary>
        /// Initializes a new instance of <see cref="FilterDesc"/>
        /// </summary>
        public FilterDesc(string? id, string title)
        {
            Id = id;
            Title = title;
        }

        /// <summary>
        /// Creates default filter
        /// </summary>
        public FilterDesc CreateDefault(string title)
        {
            return new FilterDesc(null, title);
        }
    }
}
