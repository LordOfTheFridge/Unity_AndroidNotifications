using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Notifications;

namespace DemoAndroidNotifications
{
    public class NotificationSettingsMenu : MonoBehaviour
    {
        public Action<NotificationRepeaterModel> NotificationSettingsInputed;
        public Func<NotificationRepeaterModel> RequestSettingsDb;

        [SerializeField] private TMP_Dropdown Repeater;
        [SerializeField] private GameObject PanelDateTimeSettings;
        [SerializeField] private TMP_Dropdown DateOnceAMonth;
        [SerializeField] private TMP_Dropdown DateOne;
        [SerializeField] private TMP_Dropdown DateTwo;
        [SerializeField] private TMP_Dropdown DateWeek;
        [SerializeField] private TMP_Dropdown TimeOne;
        [SerializeField] private TMP_Dropdown TimeTwo;
        [SerializeField] private Button ButtonApply;

        private NotificationRepeaterModel repeaterModel;

        void Start()
        {
            Repeater.onValueChanged.AddListener(OnRepeaterValueChanged);
            DateOnceAMonth.onValueChanged.AddListener(OnDateOnceAMonthValueChanged);
            DateOne.onValueChanged.AddListener(OnDateOneValueChanged);
            DateTwo.onValueChanged.AddListener(OnDateTwoValueChanged);
            DateWeek.onValueChanged.AddListener(OnDateWeekValueChanged);
            TimeOne.onValueChanged.AddListener(OnTimeOneValueChanged);
            TimeTwo.onValueChanged.AddListener(OnTimeTwoValueChanged);
            ButtonApply.onClick.AddListener(OnClickButtonApply);
        }

        void OnEnable()
        {
            var result = RequestSettingsDb?.Invoke();
            if (result != null) {
                repeaterModel = result;
                OnRepeaterValueChanged((int)repeaterModel.Type);
                ReconfigureUIForLocalData();
                OnInteracts();
            }
        }

        private void ReconfigureUIForLocalData()
        {
            Repeater.value = (int)repeaterModel.Type;
            switch (repeaterModel.Type) {
                case RepeaterType.None:
                    break;
                case RepeaterType.OnceAMonth:
                    DateOnceAMonth.value = repeaterModel.DateOne;
                    TimeOne.value = repeaterModel.TimeOne;
                    break;
                case RepeaterType.TwiceAMonth:
                    DateOne.value = repeaterModel.DateOne;
                    DateTwo.value = repeaterModel.DateTwo;
                    TimeOne.value = repeaterModel.TimeOne;
                    TimeTwo.value = repeaterModel.TimeTwo;
                    break;
                case RepeaterType.OnceAWeek:
                    DateWeek.value = repeaterModel.DateWeek;
                    TimeOne.value = repeaterModel.TimeOne;
                    break;
            }
        }

        private void OnRepeaterValueChanged(int value)
        {
            repeaterModel.Type = (RepeaterType)value;
            switch (repeaterModel.Type) {
                case RepeaterType.None:
                    PanelDateTimeSettings.SetActive(false);
                    break;
                case RepeaterType.OnceAMonth:
                    PanelDateTimeSettings.SetActive(true);
                    DateOnceAMonth.gameObject.SetActive(true);
                    DateOne.gameObject.SetActive(false);
                    DateTwo.gameObject.SetActive(false);
                    DateWeek.gameObject.SetActive(false);
                    TimeOne.gameObject.SetActive(true);
                    TimeTwo.gameObject.SetActive(false);
                    break;
                case RepeaterType.TwiceAMonth:
                    PanelDateTimeSettings.SetActive(true);
                    DateOnceAMonth.gameObject.SetActive(false);
                    DateOne.gameObject.SetActive(true);
                    DateTwo.gameObject.SetActive(true);
                    DateWeek.gameObject.SetActive(false);
                    TimeOne.gameObject.SetActive(true);
                    TimeTwo.gameObject.SetActive(true);
                    break;
                case RepeaterType.OnceAWeek:
                    PanelDateTimeSettings.SetActive(true);
                    DateOnceAMonth.gameObject.SetActive(false);
                    DateOne.gameObject.SetActive(false);
                    DateTwo.gameObject.SetActive(false);
                    DateWeek.gameObject.SetActive(true);
                    TimeOne.gameObject.SetActive(true);
                    TimeTwo.gameObject.SetActive(false);
                    break;
            }
        }

        private void OnDateOnceAMonthValueChanged(int value)
        {
            repeaterModel.DateOne = value;
        }

        private void OnDateOneValueChanged(int value)
        {
            repeaterModel.DateOne = value;
        }

        private void OnDateTwoValueChanged(int value)
        {
            repeaterModel.DateTwo = value;
        }

        private void OnDateWeekValueChanged(int value)
        {
            repeaterModel.DateWeek = value;
        }

        private void OnTimeOneValueChanged(int value)
        {
            repeaterModel.TimeOne = value;
        }

        private void OnTimeTwoValueChanged(int value)
        {
            repeaterModel.TimeTwo = value;
        }

        private void OnClickButtonApply()
        {
            switch (repeaterModel.Type) {
                case RepeaterType.None:
                    repeaterModel.DateOne = 0;
                    repeaterModel.DateTwo = 0;
                    repeaterModel.DateWeek = 0;
                    repeaterModel.TimeOne = 0;
                    repeaterModel.TimeTwo = 0;
                    break;
                case RepeaterType.OnceAMonth:
                    if (
                        repeaterModel.DateOne < 0 || 
                        repeaterModel.DateOne >= DateOne.options.Count || 
                        repeaterModel.TimeOne < 0 || 
                        repeaterModel.TimeOne >= TimeOne.options.Count
                    ) {
                        return;
                    }
                    repeaterModel.DateTwo = 0;
                    repeaterModel.DateWeek = 0;
                    repeaterModel.TimeTwo = 0;
                    break;
                case RepeaterType.TwiceAMonth:
                    if (
                        repeaterModel.DateOne < 0 || 
                        repeaterModel.DateOne >= DateOne.options.Count || 
                        repeaterModel.TimeOne < 0 || 
                        repeaterModel.TimeOne >= TimeOne.options.Count
                    ) {
                        return;
                    }
                    if (
                        repeaterModel.DateTwo < 0 || 
                        repeaterModel.DateTwo >= DateTwo.options.Count || 
                        repeaterModel.TimeTwo < 0 || 
                        repeaterModel.TimeTwo >= TimeTwo.options.Count
                    ) {
                        return;
                    }
                    repeaterModel.DateWeek = 0;
                    break;
                case RepeaterType.OnceAWeek:
                    if (
                        repeaterModel.DateWeek < 0 || 
                        repeaterModel.DateWeek >= DateWeek.options.Count ||
                        repeaterModel.TimeOne < 0 || 
                        repeaterModel.TimeOne >= TimeOne.options.Count
                    ) {
                        return;
                    }
                    repeaterModel.DateOne = 0;
                    repeaterModel.DateTwo = 0;
                    repeaterModel.TimeTwo = 0;
                    break;
            }

            NotificationSettingsInputed?.Invoke(repeaterModel);
        }

        private void OnInteracts()
        {
            Repeater.interactable = true;
            DateOnceAMonth.interactable = true;
            DateOne.interactable = true;
            DateTwo.interactable = true;
            DateWeek.interactable = true;
            TimeOne.interactable = true;
            TimeTwo.interactable = true;
            ButtonApply.interactable = true;
        }

        private void OffInteracts()
        {
            Repeater.interactable = false;
            DateOnceAMonth.interactable = false;
            DateOne.interactable = false;
            DateTwo.interactable = false;
            DateWeek.interactable = false;
            TimeOne.interactable = false;
            TimeTwo.interactable = false;
            ButtonApply.interactable = false;
        }

        void OnDestroy()
        {
            Repeater.onValueChanged.RemoveListener(OnRepeaterValueChanged);
            DateOnceAMonth.onValueChanged.RemoveListener(OnDateOnceAMonthValueChanged);
            DateOne.onValueChanged.RemoveListener(OnDateOneValueChanged);
            DateTwo.onValueChanged.RemoveListener(OnDateTwoValueChanged);
            DateWeek.onValueChanged.RemoveListener(OnDateWeekValueChanged);
            TimeOne.onValueChanged.RemoveListener(OnTimeOneValueChanged);
            TimeTwo.onValueChanged.RemoveListener(OnTimeTwoValueChanged);
            ButtonApply.onClick.RemoveListener(OnClickButtonApply);
        }
    }
}
