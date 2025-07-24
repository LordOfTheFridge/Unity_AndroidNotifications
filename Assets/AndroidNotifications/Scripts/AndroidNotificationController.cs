using UnityEngine;
using System;
using Unity.Notifications.Android;

namespace Utilities.Notifications
{
    public class AndroidNotificationController : MonoBehaviour
    {
        [SerializeField] private string ChannelId = "channel_id";
        [SerializeField] private string ChannelName = "Default Channel";
        [SerializeField] private string ChannelDescription = "Generic notification";
        [SerializeField] private Importance ChannelImportance = Importance.Default;

        [Space]
        [SerializeField] private string NotificationTitle = "Title";
        [SerializeField] private string NotificationText = "Notification Text";
        [SerializeField] private string NotificationSmallIcon = "icon_0";
        [SerializeField] private string NotificationLargeIcon = "icon_1";

        private AndroidNotificationChannel channel;

        public void RefreshChannel(bool clearOtherNotifications = true)
        {
            channel = FindOrCreateChannel();
            if (clearOtherNotifications) {
                ClearAll();
            }
        }

        public void CreateNotification(DateTime fireTime)
        {
            CreateNotification(NotificationTitle, NotificationText, NotificationSmallIcon, NotificationLargeIcon, fireTime);
        }

        public void CreateNotification(string notificationTitle, string notificationText, string notificationSmallIcon, string notificationLargeIcon, DateTime fireTime)
        {
            var notification = new AndroidNotification()
            {
                Title = notificationTitle,
                Text = notificationText,
                SmallIcon = notificationSmallIcon,
                LargeIcon = notificationLargeIcon,
                FireTime = fireTime
            };

            AndroidNotificationCenter.SendNotification(notification, ChannelId);
        }

        private AndroidNotificationChannel FindOrCreateChannel()
        {
            var channel = AndroidNotificationCenter.GetNotificationChannel(ChannelId);
            if (string.IsNullOrEmpty(channel.Id)) {
                channel = CreateNotificationChannel(ChannelId, ChannelName, ChannelDescription, ChannelImportance);
            }
            return channel;
        }

        private void ClearAll()
        {
            AndroidNotificationCenter.CancelAllNotifications();
        }

        private AndroidNotificationChannel CreateNotificationChannel(string id, string name, string description, Importance importance)
        {
            var channel = new AndroidNotificationChannel()
            {
                Id = id,
                Name = name,
                Description = description,
                Importance = importance
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
            return channel;
        }
    }
}
