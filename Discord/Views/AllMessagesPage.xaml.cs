using CommunityToolkit.Mvvm.ComponentModel;
using Discord.Settings;
using System;
using System.Drawing;
using System.IO;

namespace Discord.Views;

public partial class AllMessagesPage : ContentPage
{
    public AllMessagesPage()
    {
        InitializeComponent();
        LoadSideBarAsync();
        
    }
    private async Task LoadSideBarAsync()
    {
        //List<string> test = await APIEndpoints.GetMyServers();
        //var servers = await APIEndpoints.GetServers(test);

        //Console.WriteLine("daw");

        //byte[46875] imageBytes = servers[].ServerIcon;
        ////ImageSourceConverter img = new ImageSourceConverter();
        //46875 Bytes

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