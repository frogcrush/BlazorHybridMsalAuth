using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorHybridMsalAuth.Services;

// https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/security/?view=aspnetcore-9.0&pivots=maui
public class MsalAuthenticationStateProvider : AuthenticationStateProvider
{
    private AuthenticationState currentUser;

    public MsalAuthenticationStateProvider(MSALClientHelper msalHelper)
    {
        if (msalHelper.CurrentUser == null)
        {
            currentUser = new AuthenticationState(new System.Security.Claims.ClaimsPrincipal());
        }
        else
        {
            currentUser = new AuthenticationState(msalHelper.CurrentUser);
        }

        msalHelper.UserChanged += (sender, e) =>
        {
            currentUser = new AuthenticationState(e.NewUser);
            NotifyAuthenticationStateChanged(Task.FromResult(currentUser));
        };
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
        Task.FromResult(currentUser);
}