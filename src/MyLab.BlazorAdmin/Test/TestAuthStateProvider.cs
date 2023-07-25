using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
namespace MyLab.BlazorAdmin.Test;

class TestAuthStateProvider : AuthenticationStateProvider, IRemoteAuthenticationService<RemoteAuthenticationState>
{
    private readonly TestIdentity _testIdentity;
    private readonly NavigationManager _navigationManager;

    public TestAuthStateProvider(TestIdentity testIdentity, NavigationManager navigationManager)
    {
        _testIdentity = testIdentity;
        _navigationManager = navigationManager;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new GenericIdentity(_testIdentity.Name);

        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }

    public Task<RemoteAuthenticationResult<RemoteAuthenticationState>> SignInAsync(RemoteAuthenticationContext<RemoteAuthenticationState> context)
    {
        throw new NotImplementedException();
    }

    public Task<RemoteAuthenticationResult<RemoteAuthenticationState>> CompleteSignInAsync(RemoteAuthenticationContext<RemoteAuthenticationState> context)
    {
        throw new NotImplementedException();
    }

    public Task<RemoteAuthenticationResult<RemoteAuthenticationState>> SignOutAsync(RemoteAuthenticationContext<RemoteAuthenticationState> context)
    {
        _navigationManager.NavigateTo("authentication/logged-out");

        return Task.FromResult(new RemoteAuthenticationResult<RemoteAuthenticationState>
        {
            Status = RemoteAuthenticationStatus.Success
        });
    }

    public Task<RemoteAuthenticationResult<RemoteAuthenticationState>> CompleteSignOutAsync(RemoteAuthenticationContext<RemoteAuthenticationState> context)
    {
        throw new NotImplementedException();
    }
}