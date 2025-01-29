using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace BlazorHybridMsalAuth.Platforms.Android
{
    [Activity(Exported = true)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
        DataHost = "auth",
        DataScheme = "msal{MSAL_Application_Key}")]
    public class MsalActivity : BrowserTabActivity
    {
    }
}