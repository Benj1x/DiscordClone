using Discord.Settings;

namespace Discord.Views;

public partial class LoginPage : ContentPage
{
    string email = string.Empty;
    string password = string.Empty;
    APIEndpoints _api = new APIEndpoints();
    public LoginPage()
	{
		InitializeComponent();
        //IAccount cachedUserAccount = PublicClientSingleton.Instance.MSALClientHelper.FetchSignedInUserFromCache().Result;
        _ = Dispatcher.DispatchAsync(async () =>
        {
            //if (cachedUserAccount == null)
            //{
            //    SignInButton.IsEnabled = true;
            //}
            //else
            //{
            //    await Shell.Current.GoToAsync("claimsview");
            //}
        });
    }

    private async void OnLogInClicked(object sender, EventArgs e)
    {
        LogInButton.IsEnabled = false;
        //var _res = await _api.TryLogin(email, password);
        var _res = await _api.TryLogin("email@email.com", "dwasdaw");
        if (_res == null || !_res.IsSuccessStatusCode)
        {
            ErrorLbl.IsVisible = false;
            //report error
            LogInButton.IsEnabled = true;
        }
        var authSingleton = AuthSingleton.GetAuthSingleton();
        string tempstring = await _res.Content.ReadAsStringAsync();

        //todo find a better way
        var stringarray = tempstring.Split('\n');
        authSingleton.SetToken(stringarray[0].Trim());
        authSingleton.SetUserID(stringarray[1].Trim());
        //-------------
        await Shell.Current.GoToAsync("allmessagespage");


    }
    protected async void OnEmailTextChanged(object sender, TextChangedEventArgs e)
    {
        email = e.NewTextValue;
        
    }
    protected async void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
    {
        password = e.NewTextValue;
    }
    protected override bool OnBackButtonPressed() { return true; }
}