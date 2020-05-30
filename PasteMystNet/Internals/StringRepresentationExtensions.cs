using System;

namespace PasteMystNet.Internals
{

    internal static class StringRepresentationExtensions
    {

        public static string GetStringRepresentation(this Enum @enum)
        {
            var type = @enum.GetType();
            var info = type.GetMember(@enum.ToString());
            if (info.Length <= 0)
                return @enum.ToString();
            var attributes = info[0].GetCustomAttributes(typeof(StringRepresentationAttribute), false);
            return attributes.Length > 0 ? ((StringRepresentationAttribute)attributes[0]).Representation : @enum.ToString();
        }

        public static PasteMystExpiration StringToExpiration(string value)
        {
            foreach (PasteMystExpiration entry in Enum.GetValues(typeof(PasteMystExpiration)))
                if (entry.GetStringRepresentation() == value)
                    return entry;
            return PasteMystExpiration.Never;
        }

        public static PasteMystLanguage StringToLanguage(string value)
        {
            foreach (PasteMystLanguage entry in Enum.GetValues(typeof(PasteMystLanguage)))
                if (entry.GetStringRepresentation() == value)
                    return entry;
            return PasteMystLanguage.Plaintext;
        }

    }

}