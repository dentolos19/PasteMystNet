using System;

namespace PasteMystNet.Internals
{
    
    internal static class StringRepresentationExtension
    {
        
        public static string GetStringRepresentation(this Enum language)
        {
            var type = language.GetType();
            var info = type.GetMember(language.ToString());
            if (info.Length <= 0)
                return language.ToString();
            var attributes = info[0].GetCustomAttributes(typeof(StringRepresentationAttribute), false);
            return attributes.Length > 0 ? ((StringRepresentationAttribute)attributes[0]).Representation : language.ToString();
        }
        
    }
    
}