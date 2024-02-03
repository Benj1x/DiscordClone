using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Models
{
    internal class Server
    {
        public string ServerName { get; set; }
        public byte[] image { get; set; } 

        //Here would be a variable to show people in the server

    //public Server()
    //{
    //    Filename = $"{Path.GetRandomFileName()}.notes.txt";
    //    Date = DateTime.Now;
    //    Text = "";
    //}

    //public void Save() =>
    //    File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, Filename), Text);

    //public void Delete() =>
    //    File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, Filename));

    //public static Server Load(string filename)
    //{
    //    filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

    //    if (!File.Exists(filename))
    //        throw new FileNotFoundException("Unable to find file on local storage.", filename);

    //    return
    //        new()
    //        {
    //            Filename = Path.GetFileName(filename),
    //            Text = File.ReadAllText(filename),
    //            Date = File.GetLastWriteTime(filename)
    //        };
    //}
    //public static IEnumerable<Server> LoadAll()
    //{
    //    // Get the folder where the notes are stored.
    //    string appDataPath = FileSystem.AppDataDirectory;

    //    // Use Linq extensions to load the *.notes.txt files.
    //    return Directory

    //            // Select the file names from the directory
    //            .EnumerateFiles(appDataPath, "*.notes.txt")

    //            // Each file name is used to load a note
    //            .Select(filename => Note.Load(Path.GetFileName(filename)))

    //            // With the final collection of notes, order them by date
    //            .OrderByDescending(note => note.Date);
    //}
}
}
