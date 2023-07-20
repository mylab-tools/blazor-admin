using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Components.Authorization;

namespace MyLab.BlazorAdmin.Test;

class TestAuthStateProvider : AuthenticationStateProvider
{
    private readonly TestIdentity _testIdentity;

    public TestAuthStateProvider(TestIdentity testIdentity)
    {
        _testIdentity = testIdentity;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new GenericIdentity("foo");

        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }
}