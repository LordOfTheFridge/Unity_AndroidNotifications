using System;

namespace Utilities.Notifications
{
    public static class NotificationDateTimeUtilities
    {
        public static int WeekToFirstMonday(int firstSunday)
        {
            firstSunday -= 1;
            if (firstSunday == -1) { firstSunday = 6; }
            return firstSunday;
        }

        public static int WeekToFirstMonday(DayOfWeek firstSunday)
        {
            return WeekToFirstMonday((int)firstSunday);
        }

        public static int WeekToFirstSunday(int firstMonday)
        {
            firstMonday += 1;
            if (firstMonday == 7) { firstMonday = 0; }
            return firstMonday;
        }

        public static DateTime GetNextDayOfWeek(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public static DateTime GetNextDayOfWeekGivenHour(DateTime start, DayOfWeek day, int hour)
        {
            var next = GetNextDayOfWeek(start, day);
            if (next.Day == start.Day) {
                if (next.Hour < hour) {
                    return next;
                } else {
                    return GetNextDayOfWeekGivenHour(next.AddDays(1), day, hour);
                }
            } else {
                return next;
            }
        }
    }
}
