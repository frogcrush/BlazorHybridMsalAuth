using BlazorHybridMsalAuth.Services;

namespace BlazorHybridMsalAuth;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var authService = MauiProgram.Services!.GetRequiredService<MSALClientHelper>();
        var result = await authService.SignInUserInteractivelyAsync(authService.AzureAdConfig.Scopes);

        if (result != null)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}