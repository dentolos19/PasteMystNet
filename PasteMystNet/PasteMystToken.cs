namespace PasteMystNet
{
    
    public class PasteMystToken
    {

        public PasteMystToken(string token)
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