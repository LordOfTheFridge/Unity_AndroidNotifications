using UnityEngine;

namespace DemoAndroidNotifications
{
    public class PrefsDataStorage : MonoBehaviour
    {
        private const string RepeaterName = "Repeater";
        private const string MidtermName = "Midterm";
        private const string DateOneName = "DateOne";
        private const string DateTwoName = "DateTwo";
        private const string DateWeekName = "DateWeek";
        private const string TimeOneName = "TimeOne";
        private const string TimeTwoName = "TimeTwo";

        public bool IsNotificationSettingsSaved()
        {
            return 
                PlayerPrefs.HasKey(RepeaterName) && 
                PlayerPrefs.HasKey(MidtermName) && 
                PlayerPrefs.HasKey(DateOneName) && 
                PlayerPrefs.HasKey(DateTwoName) &&
                PlayerPrefs.HasKey(DateWeekName) &&
                PlayerPrefs.HasKey(TimeOneName) &&
                PlayerPrefs.HasKey(TimeTwoName);
        }

        public string[] LoadNotificationSettings()
        {
            var result = new string[7];
            result[0] = PlayerPrefs.GetString(RepeaterName);
            result[1] = PlayerPrefs.GetString(MidtermName);
            result[2] = PlayerPrefs.GetString(DateOneName);
            result[3] = PlayerPrefs.GetString(DateTwoName);
            result[4] = PlayerPrefs.GetString(DateWeekName);
            result[5] = PlayerPrefs.GetString(TimeOneName);
            result[6] = PlayerPrefs.GetString(TimeTwoName);
            return result;
        }

        public void SaveNotificationSettings(string[] settings)
        {
            PlayerPrefs.SetString(RepeaterName, settings[0]);
            PlayerPrefs.SetString(MidtermName,  settings[1]);
            PlayerPrefs.SetString(DateOneName,  settings[2]);
            PlayerPrefs.SetString(DateTwoName,  settings[3]);
            PlayerPrefs.SetString(DateWeekName, settings[4]);
            PlayerPrefs.SetString(TimeOneName,  settings[5]);
            PlayerPrefs.SetString(TimeTwoName,  settings[6]);
        }
    }
}
