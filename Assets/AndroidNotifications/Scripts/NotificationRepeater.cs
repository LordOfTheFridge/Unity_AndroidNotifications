using System;
using System.Globalization;
using UnityEngine;

namespace Utilities.Notifications
{
    public class NotificationRepeater : MonoBehaviour
    {
        [SerializeField] private int MonthsInAdvanceCount = 6;
        [SerializeField] private AndroidNotificationController NotificationController;

        public const string MidtermDateFormat = "MM/dd/yyyy";

        public void SwitchNotifications(NotificationRepeaterModel repeaterModel)
        {
            NotificationController.RefreshChannel();
            switch (repeaterModel.Type) {
                case RepeaterType.None:
                    break;
                case RepeaterType.OnceAMonth:
                    CreateNotificationsOnceAMonth(repeaterModel);
                    break;
                case RepeaterType.TwiceAMonth:
                    CreateNotificationsTwiceAMonth(repeaterModel);
                    break;
                case RepeaterType.OnceAWeek:
                    CreateNotificationsOnceAWeek(repeaterModel);
                    break;
            }
        }

        public bool IsNeedUpdateNotifications(string midterm)
        {
            if (string.IsNullOrWhiteSpace(midterm)) {
                return false;
            }
            var midtermDate = DateTime.ParseExact(midterm, MidtermDateFormat, CultureInfo.InvariantCulture);
            var compare = DateTime.Compare(midtermDate, DateTime.Now);
            if (compare <= 0) {
                return true;
            } else {
                return false;
            }
        }

        public void CreateNotificationsOnceAMonth(NotificationRepeaterModel repeaterModel)
        {
            DateTime previousDateTime;

            if (
                DateTime.Now.Day < repeaterModel.DateOne + 1 ||
                (DateTime.Now.Day == repeaterModel.DateOne + 1 && DateTime.Now.TimeOfDay.Hours < repeaterModel.TimeOne)
            ) {
                previousDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, repeaterModel.DateOne + 1, repeaterModel.TimeOne, 0, 0);
            } else {
                previousDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, repeaterModel.DateOne + 1, repeaterModel.TimeOne, 0, 0);
            }
            NotificationController.CreateNotification(previousDateTime);

            for (var i = 1; i < MonthsInAdvanceCount; i++) {
                previousDateTime = previousDateTime.AddMonths(1);
                NotificationController.CreateNotification(previousDateTime);

                if (i == MonthsInAdvanceCount / 2) {
                    repeaterModel.MidtermDate = previousDateTime.ToString(MidtermDateFormat, CultureInfo.InvariantCulture);
                }
            }
        }

        public void CreateNotificationsTwiceAMonth(NotificationRepeaterModel repeaterModel)
        {
            DateTime previousDateTime1;
            DateTime previousDateTime2;

            if (
                DateTime.Now.Day < repeaterModel.DateOne + 1 ||
                (DateTime.Now.Day == repeaterModel.DateOne + 1 && DateTime.Now.TimeOfDay.Hours < repeaterModel.TimeOne)
            ) {
                previousDateTime1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, repeaterModel.DateOne + 1, repeaterModel.TimeOne, 0, 0);
            } else {
                previousDateTime1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, repeaterModel.DateOne + 1, repeaterModel.TimeOne, 0, 0);
            }
            NotificationController.CreateNotification(previousDateTime1);

            if (
                DateTime.Now.Day < repeaterModel.DateTwo + 1 ||
                (DateTime.Now.Day == repeaterModel.DateTwo + 1 && DateTime.Now.TimeOfDay.Hours < repeaterModel.TimeTwo)
            ) {
                previousDateTime2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, repeaterModel.DateTwo + 1, repeaterModel.TimeTwo, 0, 0);
            } else {
                previousDateTime2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, repeaterModel.DateTwo + 1, repeaterModel.TimeTwo, 0, 0);
            }
            NotificationController.CreateNotification(previousDateTime2);

            for (var i = 1; i < MonthsInAdvanceCount; i++) {
                previousDateTime1 = previousDateTime1.AddMonths(1);
                NotificationController.CreateNotification(previousDateTime1);
                previousDateTime2 = previousDateTime2.AddMonths(1);
                NotificationController.CreateNotification(previousDateTime2);

                if (i == MonthsInAdvanceCount / 2) {
                    repeaterModel.MidtermDate = previousDateTime1.ToString(MidtermDateFormat, CultureInfo.InvariantCulture);
                }
            }
        }

        public void CreateNotificationsOnceAWeek(NotificationRepeaterModel repeaterModel)
        {
            DateTime previousDateTime = NotificationDateTimeUtilities.GetNextDayOfWeekGivenHour(DateTime.Now, (DayOfWeek)NotificationDateTimeUtilities.WeekToFirstSunday(repeaterModel.DateWeek), repeaterModel.TimeOne);

            NotificationController.CreateNotification(previousDateTime);

            var weekCount = (int)Math.Ceiling(MonthsInAdvanceCount * 30f / 7f);
            for (var i = 1; i < weekCount; i++) {
                previousDateTime = previousDateTime.AddDays(7);
                NotificationController.CreateNotification(previousDateTime);

                if (i == weekCount / 2) {
                    repeaterModel.MidtermDate = previousDateTime.ToString(MidtermDateFormat, CultureInfo.InvariantCulture);
                }
            }
        }
    }
}
