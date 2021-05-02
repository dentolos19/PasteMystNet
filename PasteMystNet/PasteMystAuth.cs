namespace PasteMystNet
{

    /// <summary>This class is used to contain the authorization token for accessing private pastes.</summary>
    public class PasteMystAuth
    {

        public PasteMystAuth(string token)
        {
            Token = token;
        }

        /// <summary>The authentication token for accessing private pastes.</summary>
        public string Token { get; }

        public override string ToString()
        {
            return Token;
        }

    }

}