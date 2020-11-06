using System.Net.Http.Headers;

namespace PasteMystNet
{

    public class PasteMystAuth
    {

        public PasteMystAuth(string username, string key)
        {
            Username = username;
            Key = key;
        }

        public string Username { get; }
        public string Key { get; }

        internal AuthenticationHeaderValue CreateAuthorization()
        {
            return new AuthenticationHeaderValue(Username, Key);
        }

    }

}