using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Discord.Models;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Discord.ViewModels;

internal class MainViewModel : IQueryAttributable
{
    public ObservableCollection<ViewModels.MessageViewModel> AllMessages { get; }
    public ObservableCollection<ViewModels.ServerSidebarViewModel> AllServerIcons { get; set; } = new ObservableCollection<ServerSidebarViewModel>();

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
        //Delete
        //AllNotes = new ObservableCollection<ViewModels.NoteViewModel>(Models.Note.LoadAll().Select(n => new NoteViewModel(n)));
        //NewCommand = new AsyncRelayCommand(NewNoteAsync);
        //SelectNoteCommand = new AsyncRelayCommand<ViewModels.NoteViewModel>(SelectNoteAsync);
        //
        AllMessages = new ObservableCollection<ViewModels.MessageViewModel>(Models.Message.LoadAll().Select(n => new MessageViewModel(n)));
        //AllServerIcons = new ObservableCollection<ServerSidebarViewModel>(ServerSidebar.LoadAllAsync().Result.ToList().Select(n => new ServerSidebarViewModel(n)));
        test();
        Task.Delay(500).Wait();
        Console.WriteLine("dwadaw");
        //NewCommand = new AsyncRelayCommand(NewNoteAsync);
        //SelectMessageCommand = new AsyncRelayCommand<ViewModels.MessageViewModel>(SelectMessageAsync);
        DeleteCommand = new AsyncRelayCommand(Delete);
        
    }
    /*private async Task NewNoteAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.NotePage));
    }

    private async Task SelectNoteAsync(ViewModels.NoteViewModel note)
    {
        if (note != null)
            await Shell.Current.GoToAsync($"{nameof(Views.NotePage)}?load={note.Identifier}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted"))
        {
            string noteId = query["deleted"].ToString();
            NoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

            // If note exists, delete it
            if (matchedNote != null)
                AllNotes.Remove(matchedNote);
        }
        else if (query.ContainsKey("saved"))
        {
            string noteId = query["saved"].ToString();
            NoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

            // If note is found, update it
            if (matchedNote != null)
            {
                matchedNote.Reload();
                AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
            }
            // If note isn't found, it's new; add it.
            else
                AllNotes.Insert(0, new NoteViewModel(Models.Note.Load(noteId)));
        }
    }*/
    
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
            AllServerIcons.Add(item);
            item.Reload();
        }
    }

    //private async Task NewNoteAsync()
    //{
    //    await Shell.Current.GoToAsync(nameof(Views.NotePage));
    //}

    //private async Task SelectMessageAsync(ViewModels.MessageViewModel message)
    //{
    //    if (message != null)
    //    {
    //        await Shell.Current.GoToAsync($"{nameof(Views.NotePage)}?load={message.Identifier}");
    //    }
    //}

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
        if (query.ContainsKey("load"))
        {
            var content = query["load"].ToString().Split('+');
            string serverID = content[0];
            string serverName = content[1];
            byte[] img = Convert.FromBase64String(content[2]);

            ServerSidebarViewModel matchedServer = AllServerIcons.Where((n) => n.ServerID == serverID).FirstOrDefault();
            // If note is found, update it
            if (matchedServer != null)
            {
                matchedServer.Reload();
                AllServerIcons.Move(AllServerIcons.IndexOf(matchedServer), 0);
            }
            else
            AllServerIcons.Insert(0, new ServerSidebarViewModel(ServerSidebar.Load(serverID, serverName, img)));
                
        }
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
