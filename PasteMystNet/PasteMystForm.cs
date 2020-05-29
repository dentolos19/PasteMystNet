namespace PasteMystNet
{
    
    public class PasteMystForm
    {
        
        public string Code { get; set; }
        
        public PasteMystExpire Expiration { get; set; } = PasteMystExpire.Never;

        public PasteMystLanguage Language { get; set; } = PasteMystLanguage.PlainText;

    }
    
}