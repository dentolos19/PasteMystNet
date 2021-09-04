using System.Drawing;
using System.Globalization;

namespace PasteMystNet.Internals
{

    internal static class Utilities
    {

        public static Color ParseColor(string hexadecimal)
        {
            hexadecimal = hexadecimal.TrimStart('#');
            return hexadecimal.Length == 6
                ? Color.FromArgb(255, int.Parse(hexadecimal.Substring(0, 2), NumberStyles.HexNumber), int.Parse(hexadecimal.Substring(2, 2), NumberStyles.HexNumber), int.Parse(hexadecimal.Substring(4, 2), NumberStyles.HexNumber))
                : Color.FromArgb(int.Parse(hexadecimal.Substring(0, 2), NumberStyles.HexNumber), int.Parse(hexadecimal.Substring(2, 2), NumberStyles.HexNumber), int.Parse(hexadecimal.Substring(4, 2), NumberStyles.HexNumber), int.Parse(hexadecimal.Substring(6, 2), NumberStyles.HexNumber));
        }

    }

}