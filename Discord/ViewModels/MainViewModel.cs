using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Discord.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Discord.ViewModels;

internal class MainViewModel : IQueryAttributable
{
    public ObservableCollection<ViewModels.MessageViewModel> AllMessages { get; }
    public ObservableCollection<ViewModels.ServerSidebarViewModel> AllServerIcons { get; set; }
    public List<ServerSidebarViewModel> Servers { get; set; } = new List<ServerSidebarViewModel>();
    
    Entry entry = new Entry { Placeholder = "Enter text here" };

    public Color myServersBackgroundColor { get; set; } = Color.FromRgba(30, 31, 34, 255);
    public Color myChannelsBackgroundColor { get; set; } = Color.FromRgba(43, 45, 49, 255);

    public Color myChatBackgroundColor { get; set; } = Color.FromRgba(49, 51, 56, 255);

    public Color myTextBackgroundColor { get; set; } = Color.FromRgba(56, 58, 64, 255);
    
    public Color myMembersBackgroundColor { get; set; } = Color.FromRgba(43, 45, 49, 255);

    //public ICommand NewCommand { get; }
    //public ICommand SelectMessageCommand { get; }
    //public ICommand OnEntryTextChanged { get; }
    public ICommand DeleteCommand { get; }

    public MainViewModel()
    {
        AllMessages = new ObservableCollection<ViewModels.MessageViewModel>(Models.Message.LoadAll().Select(n => new MessageViewModel(n)));
        //AllServerIcons = new ObservableCollection<ServerSidebarViewModel>(ServerSidebar.LoadAllAsync().Result.ToList().Select(n => new ServerSidebarViewModel(n)));
        test();
        Task.Delay(5000).Wait();
        Console.WriteLine("dwadaw");
        //NewCommand = new AsyncRelayCommand(NewNoteAsync);
        //SelectMessageCommand = new AsyncRelayCommand<ViewModels.MessageViewModel>(SelectMessageAsync);
        DeleteCommand = new AsyncRelayCommand(Delete);
        
    }

    private async Task Delete()
    {
        await Shell.Current.GoToAsync($"..?deleted=test");

    }
    private async Task test()
    {
        var serverSidebars = await ServerSidebar.GetAllAsync();
        AllServerIcons = new ObservableCollection<ServerSidebarViewModel>(serverSidebars.ToList().Select(n => new ServerSidebarViewModel(n)));
        foreach (var item in AllServerIcons)
        {
            Servers.Add(item);
            item.Reload();
        }
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
        if (query.ContainsKey("joined"))
        {
            string serverID = query["joined"].ToString();
            ServerSidebarViewModel matchedServer = AllServerIcons.Where((n) => n.ServerID == serverID).FirstOrDefault();

            // If note is found, update it
            if (matchedServer != null) 
            {
                matchedServer.Reload();
                AllServerIcons.Move(AllServerIcons.IndexOf(matchedServer), 0);
            }
            else// If note isn't found, it's new; add it.
            {
                //AllServerIcons.Insert(0, new ServerSidebarViewModel(Models.ServerSidebar.Load(serverID)));
            }
        }
        if (query.ContainsKey("left"))
        {
            string messageId = query["left"].ToString();
            ServerSidebarViewModel matchedServer = AllServerIcons.Where((n) => n.ServerID == messageId).FirstOrDefault();

            // If note exists, delete it
            if (matchedServer != null)
            {
                AllServerIcons.Remove(matchedServer);
            }
        }


    }
}
