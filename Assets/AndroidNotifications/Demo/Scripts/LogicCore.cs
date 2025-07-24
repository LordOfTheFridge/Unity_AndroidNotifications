using UnityEngine;
using Utilities.Notifications;

namespace DemoAndroidNotifications
{
    public class LogicCore : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private NotificationSettingsMenu NotificationSettingsMenu;

        [Header("Notifications")]
        [SerializeField] private NotificationRepeater NotificationRepeater;
        [SerializeField] private AndroidNotificationController AndroidNotificationController;

        [Header("Data")]
        [SerializeField] private DatabaseController Database;

        void Start()
        {
            NotificationSettingsMenu.NotificationSettingsInputed += OnNotificationSettingsInputed;
            NotificationSettingsMenu.RequestSettingsDb += OnRequestSettingsDb;

            if (NotificationRepeater.IsNeedUpdateNotifications(Database.GetSettingsDb().MidtermDate)) {
                OnNotificationSettingsInputed(Database.GetSettingsDb());
            }

            ShowNotificationSettingsMenu();
        }

        private void ShowNotificationSettingsMenu()
        {
            NotificationSettingsMenu?.gameObject.SetActive(true);
        }

        private void OnNotificationSettingsInputed(NotificationRepeaterModel repeaterModel)
        {
            NotificationRepeater.SwitchNotifications(repeaterModel);
            Database.ChangeNotificationSettings(repeaterModel);
        }

        private NotificationRepeaterModel OnRequestSettingsDb()
        {
            return Database.GetSettingsDb();
        }

        void OnDestroy()
        {
            NotificationSettingsMenu.NotificationSettingsInputed -= OnNotificationSettingsInputed;
            NotificationSettingsMenu.RequestSettingsDb -= OnRequestSettingsDb;
        }
    }
}
