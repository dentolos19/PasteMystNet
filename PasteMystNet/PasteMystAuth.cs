namespace PasteMystNet
{

    /// <summary>
    /// This class is used to contain the authorization token for interacting with private pastes.
    /// </summary>
    public class PasteMystAuth
    {

        public PasteMystAuth(string token)
        {
            Token = token;
        }

        public string Token { get; }

        public override string ToString()
        {
            return Token;
        }

    }

}