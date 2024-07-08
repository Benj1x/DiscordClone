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
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        chatCollection.SelectedItem = null;
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {

    }
}