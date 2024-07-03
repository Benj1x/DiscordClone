namespace Discord
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            /* Every page that can be navigated to from another page, needs to be registered with the navigation system.
             The AllMessagesPage and AboutPage pages are automatically registered with the navigation system by being declared in the TabBar.
              So to register the 'NotePage' we have to manually register the NotesPage with the navigation system:
             */

            /*The Routing.RegisterRoute method takes two parameters:
            The first parameter is the string name of the URI you want to register, in this case the resolved name is "NotePage".
            The second parameter is the type of page to load when "NotePage" is navigated to.*/
            Routing.RegisterRoute(nameof(Views.NotePage), typeof(Views.NotePage));
            Routing.RegisterRoute("allmessagespage", typeof(Views.AllMessagesPage));
        }
    }
}
