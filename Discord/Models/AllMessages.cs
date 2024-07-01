using System.Collections.ObjectModel;

namespace Discord.Models;

internal class AllMessages
{
    public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();

    public AllMessages() =>
        LoadMessages();

    public void LoadMessages()
    {
        Messages.Clear();

        // Get the folder where the notes are stored.
        string appDataPath = FileSystem.AppDataDirectory;

        // Use Linq extensions to load the *.notes.txt files.
        IEnumerable<Message> messages = Directory

                                    // Select the file names from the directory
                                    .EnumerateFiles(appDataPath, "*.messages.txt")

                                    // Each file name is used to create a new Note
                                    .Select(filename => new Message()
                                    {
                                        Filename = filename,
                                        Text = File.ReadAllText(filename),
                                        Date = File.GetCreationTime(filename)
                                    })

                                    // With the final collection of notes, order them by date
                                    .OrderBy(note => note.Date);

        // Add each note into the ObservableCollection
        foreach (Message message in messages)
            Messages.Add(message);
    }
}