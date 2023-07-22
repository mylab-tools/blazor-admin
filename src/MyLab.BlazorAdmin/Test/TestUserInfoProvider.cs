using MyLab.BlazorAdmin.Services;
using System.Security.Principal;

namespace MyLab.BlazorAdmin.Test
{
    class TestUserInfoProvider : IUserInfoProvider
    {
        private readonly TestIdentity _testIdentity;

        public TestUserInfoProvider(TestIdentity testIdentity)
        {
            _testIdentity = testIdentity;
        }
        public Task<UserInfo> ProvideAsync(IPrincipal principal)
        {
            return Task.FromResult(
                new UserInfo(_testIdentity.Name)
                {
                    Subtitles = _testIdentity.Subtitles
                });
        }
    }
}
