using System;
using PasteMystNet.Internals;

namespace PasteMystNet
{
    
    public class PasteMystInfo
    {
        
        public string Id { get; set; }

        public DateTime Date { get; set; }
        
        public string Code { get; set; }
        
        public PasteMystExpiration Expiration { get; set; }
        
        public PasteMystLanguage Language { get; set; }

        internal static PasteMystInfo FromJson(PasteMystInfoJson json)
        {
            var info = new PasteMystInfo
            {
                Id = json.Id,
                Date = DateTimeOffset.FromUnixTimeSeconds(json.Date).DateTime,
                Code = Uri.UnescapeDataString(json.Code),
                Expiration = StringRepresentationExtensions.StringToExpiration(json.Expiration),
                Language = StringRepresentationExtensions.StringToLanguage(json.Language)
            };
            return info;
        }

    }
    
}