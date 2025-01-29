namespace BlazorHybridMsalAuth.Components.Pages
{
    public partial class Home
    {
        private string? UsersName;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthState.GetAuthenticationStateAsync();
            UsersName = authState.User.Claims.First(x => x.Type == "name").Value;
        }
    }
}