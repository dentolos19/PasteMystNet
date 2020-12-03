namespace PasteMystNet.Internals
{

    internal static class StringRepresentationExtensions
    {

        public static string GetStringRepresentation(this object @object)
        {
            var type = @object.GetType();
            var info = type.GetMember(@object.ToString());
            if (info.Length <= 0)
                return @object.ToString();
            var attributes = info[0].GetCustomAttributes(typeof(StringRepresentationAttribute), false);
            return attributes.Length > 0 ? ((StringRepresentationAttribute)attributes[0]).Representation : @object.ToString();
        }

    }

}