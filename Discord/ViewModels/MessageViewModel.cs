using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace Discord.ViewModels;

internal class MessageViewModel : ObservableObject, IQueryAttributable
{
    private Models.Message _message;
    private Models.ServerSidebar _sidebar;
    public DateTime Date => _message.Date;
    public string Identifier => _message.Filename;
    public ICommand SaveCommand { get; private set; }
    public ICommand DeleteCommand { get; private set; }
    public string Text
    {
        get => _message.Text; //the => syntax creates a get-only property where the statement to the right of => must evaluate to a value to return
        set
        {
            if (_message.Text != value)
            {
                _message.Text = value;
                OnPropertyChanged();
            }
        }
    }

    public MessageViewModel()
    {
        _message = new Models.Message();
        SaveCommand = new AsyncRelayCommand(Save);
        DeleteCommand = new AsyncRelayCommand(Delete);
    }

    public MessageViewModel(Models.Message message)
    {
        _message = message;
        SaveCommand = new AsyncRelayCommand(Save);
        DeleteCommand = new AsyncRelayCommand(Delete);
    }
    private async Task Save()
    {
        _message.Date = DateTime.Now;
        _message.Save();
        await Shell.Current.GoToAsync($"..?saved={_message.Filename}");
    }

    private async Task Delete()
    {
        _message.Delete();
        await Shell.Current.GoToAsync($"..?deleted={_message.Filename}");
    }
    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            _message = Models.Message.Load(query["load"].ToString());
            RefreshProperties();
        }
        if (query.ContainsKey("JoinedNewServer"))
        {
            //_sidebar = Models.ServerSidebar.JoinedNewServer(query["load"].ToString());
            RefreshProperties();
        }
    }
    public void Reload()
    {
        _message = Models.Message.Load(_message.Filename);
        RefreshProperties();
    }

    private void RefreshProperties()
    {
        OnPropertyChanged(nameof(Text));
        OnPropertyChanged(nameof(Date));
    }
}