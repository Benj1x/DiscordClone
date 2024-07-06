using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Discord.Models;
using System.Windows.Input;
using System.Text;

namespace Discord.ViewModels;

internal class ServerSidebarViewModel : ObservableObject, IQueryAttributable
{
    private ServerSidebar _sSidebar;

    public string ServerName
    {
        get => _sSidebar.ServerName;
        set
        {
            if (_sSidebar.ServerName != value)
            {
                _sSidebar.ServerName = value;
                OnPropertyChanged();
            }
        }
    }

    public string ServerID => _sSidebar.ServerID;

    public byte[] ServerIcon => _sSidebar.image;

    public ICommand LeaveCommand { get; private set; }

    public ServerSidebarViewModel(ServerSidebar serverSidebar)
    {
        _sSidebar = serverSidebar;
        LeaveCommand = new AsyncRelayCommand(Leave);
        LoadServer();
    }


    private async Task Leave()
    {
        _sSidebar.Leave();
        await Shell.Current.GoToAsync($"..?left={_sSidebar.ServerID}");
        //Delete from UI on 204
    }

    private async Task LoadServer()
    {
        string image = Convert.ToBase64String(_sSidebar.image);
        await Shell.Current.GoToAsync($"..?load={_sSidebar.ServerID}+{_sSidebar.ServerName}+{image}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("loadServer"))
        {
            //_sSidebar = ServerSidebar.Load(query["loadServer"].ToString());
            RefreshProperties();
        }
        //if (query.ContainsKey("loadServers"))
        //{
        //    _sSidebar = ServerSidebar.Load(query["load"].ToString());
        //    RefreshProperties();
        //}
        if (query.ContainsKey("Left"))
        {
            string serverID = query["Left"].ToString();
            //ServerSidebarViewModel matchedNote = .Where((n) => n.Identifier == noteId).FirstOrDefault();

            // If note exists, delete it
           // if (matchedNote != null)
           //     AllNotes.Remove(matchedNote);
        }
    }

    public void Reload()
    {
        _sSidebar = ServerSidebar.Load(_sSidebar);
        RefreshProperties();
    }

    private void RefreshProperties()
    {
        OnPropertyChanged(nameof(ServerName));
        OnPropertyChanged(nameof(ServerIcon));
    }
}
