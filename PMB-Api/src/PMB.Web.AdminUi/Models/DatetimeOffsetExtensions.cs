using System;

namespace PMB.Web.AdminUi.Models
{
    public static class DatetimeOffsetExtensions
    {
        public static DateTimeOffset WithDate(this DateTime dt, DateTimeOffset dtOffset)
        {
            // day + 1 - это багулина в MatBlazor, надо прибавлять всегда
            return new DateTimeOffset(dt.Year, dt.Month, dt.Day + 1, dtOffset.Hour, dtOffset.Minute, dtOffset.Second,
                dtOffset.Offset);
        }
    }
}