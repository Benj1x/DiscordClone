using Discord.Settings;
using Microsoft.Maui.Storage;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Models;

internal class ServerSidebar
{
    public string ServerName { get; set; }
    public string ServerID { get; set; }
    public byte[] image { get; set; }

    //Here would be a variable to show people in the server

    public ServerSidebar(ServerSidebar sidebar)
    {
        ServerName = sidebar.ServerName;
        ServerID = sidebar.ServerID;
        image = sidebar.image;
    }
    public ServerSidebar(string name, string id, byte[] icon)
    {
        ServerName = name;
        ServerID = id;
        image = icon;
    }

    public async void Leave()
    {
        //Leave server, on 204 return

    }

    //public static ServerSidebar JoinedNewServer(string serverID)
    //{
    //    //Do get request for one server

    //    //if request != 200-201
    //    //throw new Server Not found

    //    return
    //        new()
    //        {
    //            ServerName = //gotten serverName,
    //            serverID = //Gotten serverID
    //            image = //gotten server image
    //        };
    //    //return new ServerSidebar() { ServerName = serverID, ServerID = serverID, image = [] };
    //}

    public static ServerSidebar Load(ServerSidebar server)
    {
        return new(new ServerSidebar(server));
    }

    public static ServerSidebar Load(string serverID, string serverName, byte[] img)
    {
        return new(new ServerSidebar(serverName, serverID, img));
    }

    public static async Task<List<ServerSidebar>>? GetServers(List<string> serverIDs)
    {
        var servers = await APIEndpoints.GetServers(serverIDs);

        IEnumerable<ServerSidebar> serverIcons = servers.Select(x => new ServerSidebar(x.ServerName, x.ServerID, x.ServerIcon))
            .OrderBy(x => x.ServerName); //Change to entry date (or something dynamic?)

        var myServers = new List<ServerSidebar>();

        foreach (ServerSidebar server in serverIcons)
        {
            myServers.Add(server);
        }
        return myServers;
    }
    public static async Task<IEnumerable<ServerSidebar>> GetAllAsync()
    {
        //Get list of servers we are in
        List<string> serverIDs = await APIEndpoints.GetMyServers();

        return await GetServers(serverIDs);
    }
}

