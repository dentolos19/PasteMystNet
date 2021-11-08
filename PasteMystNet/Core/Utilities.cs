using System.Drawing;
using System.Globalization;

namespace PasteMystNet.Core
{

    public static class Utilities
    {

        public static Color ParseColorHex(string hex)
        {
            hex = hex.TrimStart('#');
            return hex.Length == 6
                ? Color.FromArgb(255, int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber), int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber), int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber))
                : Color.FromArgb(int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber), int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber), int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber), int.Parse(hex.Substring(6, 2), NumberStyles.HexNumber));
        }

    }

}