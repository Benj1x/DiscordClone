using CommunityToolkit.Mvvm.ComponentModel;
using Discord.Settings;
using System;
using System.Drawing;
using System.IO;

namespace Discord.Views;

public partial class AllMessagesPage : ContentPage
{
    APIEndpoints _api = new APIEndpoints();
    public AllMessagesPage()
    {
        InitializeComponent();
        LoadSideBarAsync();
        
    }
    private async Task LoadSideBarAsync()
    {
        
        var test = await _api.GetMyServers();
        if (test == null || !test.IsSuccessStatusCode)
        {
            return;
        }
        var test2 = test.Content.ReadAsStringAsync();
        //byte[] imageBytes = Convert.FromBase64String(base64String);
        ////ImageSourceConverter img = new ImageSourceConverter();
        

        //Image image;
        //// Create memory stream from byte array
        //using (MemoryStream ms = new MemoryStream(imageBytes))
        //{
        //    // Create image from memory stream
        //    //image = Image.
        //    ImageSource imageSource = new ImageSource;
        //    Testimg.Source = ImageSource.FromStream(ms.BeginRead());
        //}
        
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        chatCollection.SelectedItem = null;
    }
}