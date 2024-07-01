using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Discord.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Discord.ViewModels;

internal class ChatViewModel : IQueryAttributable
{
    public ObservableCollection<ViewModels.MessageViewModel> AllMessages { get; }
    public ICommand NewCommand { get; }
    public ICommand SelectMessageCommand { get; }
    Entry entry = new Entry { Placeholder = "Enter text here" };

    public Color myServersBackgroundColor { get; set; } = Color.FromRgba(30, 31, 34, 255);
    public Color myChannelsBackgroundColor { get; set; } = Color.FromRgba(43, 45, 49, 255);

    public Color myChatBackgroundColor { get; set; } = Color.FromRgba(49, 51, 56, 255);

    public Color myTextBackgroundColor { get; set; } = Color.FromRgba(56, 58, 64, 255);
    
    public Color myMembersBackgroundColor { get; set; } = Color.FromRgba(43, 45, 49, 255);

    //public ICommand OnEntryTextChanged { get; }

    public ChatViewModel()
    {
        AllMessages = new ObservableCollection<ViewModels.MessageViewModel>(Models.Message.LoadAll().Select(n => new MessageViewModel(n)));
        NewCommand = new AsyncRelayCommand(NewNoteAsync);
        SelectMessageCommand = new AsyncRelayCommand<ViewModels.MessageViewModel>(SelectMessageAsync);

    }

    private async Task NewNoteAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.NotePage));
    }

    private async Task SelectMessageAsync(ViewModels.MessageViewModel message)
    {
        if (message != null)
        {
            await Shell.Current.GoToAsync($"{nameof(Views.NotePage)}?load={message.Identifier}");
        }
    }

    void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        string oldText = e.OldTextValue;
        string newText = e.NewTextValue;
        string myText = entry.Text;
    }

    void OnEntryCompleted(object sender, EventArgs e)
    {
        string text = ((Entry)sender).Text;
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted"))
        {
            string messageId = query["deleted"].ToString();
            MessageViewModel matchedMessage = AllMessages.Where((n) => n.Identifier == messageId).FirstOrDefault();

            // If note exists, delete it
            if (matchedMessage != null)
            {
                AllMessages.Remove(matchedMessage);
            }
        }
        else if (query.ContainsKey("saved"))
        {
            string matchedId = query["saved"].ToString();
            MessageViewModel matchedMessage = AllMessages.Where((n) => n.Identifier == matchedId).FirstOrDefault();

            // If note is found, update it
            if (matchedMessage != null) 
            {
                matchedMessage.Reload();
                AllMessages.Move(AllMessages.IndexOf(matchedMessage), 0);
            }
            else// If note isn't found, it's new; add it.
            {
                AllMessages.Insert(0, new MessageViewModel(Models.Message.Load(matchedId)));
            }
            

        }
    }
}