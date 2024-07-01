using CommunityToolkit.Mvvm.ComponentModel;
using Windows.ApplicationModel.VoiceCommands;

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
}