namespace PasteMystNet
{

    public class PasteMystToken
    {

        public string Token { get; }

        public PasteMystToken(string token)
        {
            Token = token;
        }

        public override string ToString()
        {
            return Token;
        }

    }

}