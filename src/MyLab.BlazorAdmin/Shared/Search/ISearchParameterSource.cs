using MyLab.BlazorAdmin.Tools.Search;

namespace MyLab.BlazorAdmin.Shared.Search
{
    /// <summary>
    /// Defines source of search parameters
    /// </summary>
    public interface ISearchParameterSource
    {
        /// <summary>
        /// Applies parameters to a request
        /// </summary>
        void ApplyParameters(SearchRequest request);
    }
}
