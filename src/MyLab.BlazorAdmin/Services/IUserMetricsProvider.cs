using MyLab.BlazorAdmin.ComponentModel;

namespace MyLab.BlazorAdmin.Services
{
    /// <summary>
    /// Represent a service which provides dynamic user metrics
    /// </summary>
    public interface IUserMetricsProvider
    {
        /// <summary>
        /// Provides user metrics for current user
        /// </summary>
        /// <returns></returns>
        Task<UserMetric[]> ProvideAsync();
    }
    
    /// <summary>
    /// Contains user metric parameters
    /// </summary>
    /// <param name="Value">Metric value</param>
    /// <param name="FaClass">Font Awesome class</param>
    public record UserMetric(string Value, string FaClass)
    {
        /// <summary>
        /// Metrics title
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Additional link
        /// </summary>
        public Link? AdditionalLink { get; set; }
    }
}
