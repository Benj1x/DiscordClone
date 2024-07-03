
using System.Runtime.CompilerServices;

namespace Discord.Settings
{
    public sealed class AuthSingleton
    {
        private static AuthSingleton _lazyInstance = null!;
        private static readonly object Padlock = new();

        private string _token = string.Empty;
        private string _UserID = string.Empty;
        private AuthSingleton()
        {
        }
        public static AuthSingleton GetAuthSingleton()
        {
            //Make it thread safe
            lock (Padlock)
            {
                return _lazyInstance ??= new AuthSingleton();
            }
        }

        public string GetToken()
        {
            //Make it thread safe
            lock (_token)
            {
                return _token;
            }
        }

        public string GetUserID()
        {
            //Make it thread safe
            lock (_UserID)
            {
                return _UserID;
            }
        }

        public void SetToken(string token)
        {
            //Make it thread safe
            lock (_token)
            {
                _token = token;
            }
        }
        public void SetUserID(string id)
        {
            //Make it thread safe
            lock (_UserID)
            {
                _UserID = id;
            }
        }


    }
}
