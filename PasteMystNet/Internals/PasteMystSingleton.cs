using System.Net.Http;

namespace PasteMystNet.Internals
{

    internal sealed class PasteMystSingleton
    {

        private PasteMystSingleton()
        {
            HttpClient = new HttpClient();
        }

        public static PasteMystSingleton Instance { get; } = new PasteMystSingleton();

        public HttpClient HttpClient { get; }

    }

}