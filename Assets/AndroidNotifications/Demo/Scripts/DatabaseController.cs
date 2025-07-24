using UnityEngine;
using Utilities.Notifications;

namespace DemoAndroidNotifications
{
    public class DatabaseController : MonoBehaviour
    {
        [SerializeField] private PrefsDataStorage PrefsStorage;
        [SerializeField] private NotificationSettingsDatabase SettingsDb;

        void Start()
        {
            PrepareNotificationSettings();
        }

        private void PrepareNotificationSettings()
        {
            if (PrefsStorage.IsNotificationSettingsSaved()) {
                SettingsDb.ReadData(PrefsStorage.LoadNotificationSettings());
            } else {
                SettingsDb.ClearAll();
            }
        }

        public void ChangeNotificationSettings(NotificationRepeaterModel repeaterModel)
        {
            SettingsDb.ChangeRepeater(repeaterModel);
            PrefsStorage.SaveNotificationSettings(SettingsDb.GetDbInStrings());
        }

        public NotificationRepeaterModel GetSettingsDb()
        {
            return SettingsDb.GetDb();
        }
    }
}
