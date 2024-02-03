//using System.Collections.ObjectModel;
//using Discord.API;

//namespace Discord.Models
//{
//    internal class AllServers
//    {
//        public ObservableCollection<Server> Servers { get; set; } = new ObservableCollection<Server>();
//        private
//        public AllServers() =>
//            GetServers();

//        public void GetServers()
//        {
//            Servers.Clear();
//            /*
//             List<Server> servers = new List<Server>();
//             Server? res = await _httpClient.GetFromJsonAsync<Server>($"https://localhost:7210/api/Servers");
//             */
//            //Functions.GetMyServers("pik");
//            //// Get the folder where the notes are stored.
//            //string appDataPath = FileSystem.AppDataDirectory;

//            //// Use Linq extensions to load the *.notes.txt files.
//            //IEnumerable<Server> servers = Directory

//            //                            // Select the file names from the directory
//            //                            .EnumerateFiles(appDataPath, "*.notes.txt")

//            //                            // Each file name is used to create a new Note
//            //                            .Select(filename => new Server()
//            //                            {
//            //                                Filename = filename,
//            //                                Text = File.ReadAllText(filename),
//            //                                Date = File.GetCreationTime(filename)
//            //                            })

//            //                            // With the final collection of notes, order them by date
//            //                            .OrderBy(note => note.Date);

//            //// Add each note into the ObservableCollection
//            //foreach (Server server in Servers)
//            //    Servers.Add(server);
//        }
//    }
//}
