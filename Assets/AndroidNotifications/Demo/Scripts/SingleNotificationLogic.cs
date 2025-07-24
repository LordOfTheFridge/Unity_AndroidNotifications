using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Notifications;

namespace DemoAndroidNotifications
{
    public class SingleNotificationLogic : MonoBehaviour
    {
        [Header("Notification")]
        [SerializeField] private AndroidNotificationController AndroidNotificationController;

        [Header("UI")]
        [SerializeField] private Button ButtonCreate;
        [SerializeField] private TMP_InputField Title;
        [SerializeField] private TMP_InputField Text;


        private bool isChannelRefreshed = false;

        void Start()
        {
            ButtonCreate.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (!isChannelRefreshed) {
                AndroidNotificationController.RefreshChannel(clearOtherNotifications: false);
                isChannelRefreshed = true;
            }
            var fireTime = DateTime.Now.AddMinutes(2);
            AndroidNotificationController.CreateNotification(Title.text, Text.text, "icon_0", "icon_1", fireTime);

            ButtonCreate.interactable = false;
        }

        void OnDestroy()
        {
            ButtonCreate.onClick.RemoveListener(OnClick);
        }
    }
}
