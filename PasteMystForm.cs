namespace PasteMystNet
{
    
    public class PasteMystForm
    {
        
        public string Code { get; set; }
        
        public PasteMystExpiration Expiration { get; set; } = PasteMystExpiration.Never;

        public PasteMystLanguage Language { get; set; } = PasteMystLanguage.Autodetect;

    }
    
}