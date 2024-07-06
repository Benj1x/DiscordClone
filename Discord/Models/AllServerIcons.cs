using System.Collections.ObjectModel;
using Discord.Settings;
namespace Discord.Models
{
    internal class AllServers
    {
        public ObservableCollection<ServerSidebar> Servers { get; set; } = new ObservableCollection<ServerSidebar>();
        private APIEndpoints _api = new APIEndpoints();
        public AllServers() =>
            GetServers();

        public async void GetServers()
        {
            Servers.Clear();
            //List<string> test = await _api.GetMyServers();
            //var servers = await _api.GetServers(test);

            //Console.WriteLine("daw");

            //IEnumerable<ServerSidebar> serverIcons = servers.Select(x => new ServerSidebar(x)
            //{
            //    ServerName = x.ServerName,
            //    ServerID = x.ServerID,
            //    image = x.ServerIcon
            //}).OrderBy(x => x.ServerName); //Change to entry date (or something dynamic?)
            //foreach (ServerSidebar server in serverIcons)
            //{
            //    Servers.Add(server);
            //}
            /*

            //// Use Linq extensions to load the *.notes.txt files.
            //IEnumerable<Server> servers = Directory

            //                            // Select the file names from the directory
            //                            .EnumerateFiles(appDataPath, "*.notes.txt")

            //                            // Each file name is used to create a new Note
            //                            .Select(filename => new Server()
            //                            {
            //                                Filename = filename,
            //                                Text = File.ReadAllText(filename),
            //                                Date = File.GetCreationTime(filename)
            //                            })

            //                            // With the final collection of notes, order them by date
            //                            .OrderBy(note => note.Date);

            //// Add each note into the ObservableCollection
            //foreach (Server server in Servers)
            //    Servers.Add(server);*/
        }
    }
}
