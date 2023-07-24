using System.Security.Claims;
using System.Security.Principal;

namespace MyLab.BlazorAdmin.Services
{
    /// <summary>
    /// Describes a service which should provide user info from Identity
    /// </summary>
    public interface IUserInfoProvider
    {
        /// <summary>
        /// Provides user info for current user
        /// </summary>
        Task<UserInfo> ProvideAsync(IPrincipal principal);
    }

    /// <summary>
    /// Contains user info for User block
    /// </summary>
    /// <param name="Name">user name</param>
    public record UserInfo(string Name)
    {
        /// <summary>
        /// Subtitles
        /// </summary>
        public string[]? Subtitles { get; set; }
        /// <summary>
        /// User icon URL
        /// </summary>
        public string? IconUrl { get; set; }
    }

    class DefaultUserInfoProvider : IUserInfoProvider
    {
        public Task<UserInfo> ProvideAsync(IPrincipal? principal)
        {
            return Task.FromResult(new UserInfo(principal?.Identity?.Name ?? "[noname]")
            {
                IconUrl = "_content/MyLab.BlazorAdmin/img/user.png"
            });
        }
    }
}
