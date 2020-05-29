using PasteMystNet.Internals;

namespace PasteMystNet
{
    
    public enum PasteMystExpire
    {
        [StringRepresentation("never")] Never,
        [StringRepresentation("1h")] OneHour,
        [StringRepresentation("2h")] TwoHours,
        [StringRepresentation("10h")] TenHours,
        [StringRepresentation("1d")] OneDay,
        [StringRepresentation("2d")] TwoDays,
        [StringRepresentation("1w")] OneWeek,
        [StringRepresentation("1m")] OneMonth,
        [StringRepresentation("1y")] OneYear
    }
    
}