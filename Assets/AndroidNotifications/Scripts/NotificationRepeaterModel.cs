namespace Utilities.Notifications
{
    public enum RepeaterType
    {
        None,
        OnceAMonth,
        TwiceAMonth,
        OnceAWeek
    }

    public class NotificationRepeaterModel
    {
        public RepeaterType Type;
        public string MidtermDate;
        public int DateOne;
        public int DateTwo;
        public int DateWeek;
        public int TimeOne;
        public int TimeTwo;
    }
}
