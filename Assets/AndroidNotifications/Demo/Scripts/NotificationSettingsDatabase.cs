using UnityEngine;
using Utilities.Notifications;

namespace DemoAndroidNotifications
{
    public class NotificationSettingsDatabase : MonoBehaviour
    {
        private NotificationRepeaterModel database = new NotificationRepeaterModel();

        public void ReadData(string[] settings)
        {
            database.Type = (RepeaterType)int.Parse(settings[0]);
            database.MidtermDate = settings[1];
            database.DateOne = int.Parse(settings[2]);
            database.DateTwo = int.Parse(settings[3]);
            database.DateWeek = int.Parse(settings[4]);
            database.TimeOne = int.Parse(settings[5]);
            database.TimeTwo = int.Parse(settings[6]);
        }

        public void ChangeRepeater(NotificationRepeaterModel repeaterModel)
        {
            database = repeaterModel;
        }

        public string[] GetDbInStrings()
        {
            var result = new string[7];
            result[0] = ((int)database.Type).ToString();
            result[1] = database.MidtermDate;
            result[2] = database.DateOne.ToString();
            result[3] = database.DateTwo.ToString();
            result[4] = database.DateWeek.ToString();
            result[5] = database.TimeOne.ToString();
            result[6] = database.TimeTwo.ToString();
            return result;
        }

        public void ClearAll()
        {
            database.Type = RepeaterType.None;
            database.MidtermDate = "";
            database.DateOne = 0;
            database.DateTwo = 0;
            database.DateWeek = 0;
            database.TimeOne = 0;
            database.TimeTwo = 0;
        }

        public NotificationRepeaterModel GetDb()
        {
            return database;
        }
    }
}
