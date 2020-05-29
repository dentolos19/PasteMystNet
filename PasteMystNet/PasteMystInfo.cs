using System;

namespace PasteMystNet
{
    
    public class PasteMystInfo
    {
        
        public string Id { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Code { get; set; }
        
        public PasteMystExpire Expiration { get; set; }
        
        public PasteMystLanguage Language { get; set; }
        
    }
    
}