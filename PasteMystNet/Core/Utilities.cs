using System.Drawing;
using System.Globalization;

namespace PasteMystNet.Core
{

    public static class Utilities
    {

        public static Color ParseAsColorHex(this string hex)
        {
            hex = hex.TrimStart('#');
            return hex.Length == 6
                ? Color.FromArgb(255, // alpha
                    int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber), // red
                    int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber), // green
                    int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber)) // blue
                : Color.FromArgb(int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber), // alpha
                    int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber), // red
                    int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber), // green
                    int.Parse(hex.Substring(6, 2), NumberStyles.HexNumber)); // blue
        }

    }

}