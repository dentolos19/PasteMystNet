namespace PasteMystNet
{
    
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