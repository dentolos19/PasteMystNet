using System;

namespace PasteMystNet.Internals
{
    
    internal class StringRepresentationAttribute : Attribute
    {

        public string Representation { get; }
        
        public StringRepresentationAttribute(string representation)
        {
            Representation = representation;
        }

        public override string ToString()
        {
            return Representation;
        }

    }
    
}