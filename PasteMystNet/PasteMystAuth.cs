using System.Net.Http.Headers;

namespace PasteMystNet
{

    public class PasteMystAuth
    {

        public PasteMystAuth(string token)
        {
            Token = token;
        }

        public string Token { get; }

        internal AuthenticationHeaderValue CreateAuthorization()
        {
            return new AuthenticationHeaderValue("Basic", Token);
        }

    }

}