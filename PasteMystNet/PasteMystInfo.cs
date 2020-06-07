using System;
using PasteMystNet.Internals;

namespace PasteMystNet
{

    public class PasteMystInfo
    {

        public string Id { get; private set; }

        public string Link { get; private set; }

        public DateTime Date { get; private set; }

        public string Code { get; private set; }

        public PasteMystExpiration Expiration { get; private set; }

        public PasteMystLanguage Language { get; private set; }

        internal static PasteMystInfo FromJson(PasteMystInfoJson json)
        {
            var info = new PasteMystInfo
            {
                Id = json.Id,
                Link = PasteMystConstants.PmEndpoint + json.Id,
                Date = DateTimeOffset.FromUnixTimeSeconds(json.Date).DateTime,
                Code = Uri.UnescapeDataString(json.Code),
                Expiration = StringRepresentationExtensions.StringToExpiration(json.Expiration),
                Language = StringRepresentationExtensions.StringToLanguage(json.Language)
            };
            return info;
        }

    }

}