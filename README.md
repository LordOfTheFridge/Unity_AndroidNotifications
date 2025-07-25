# Unity_AndroidNotifications
This is a tool to simplify creation of notifications on Android.
This package includes:
* Android notification controller script.
* Prefab that creates repeating notifications depending on scheduled date and time.
* Demo of creating a single notification.
* Demo of notification repeater.

## Installation

Import this unity package.

## Usage
### AndroidNotificationController
This will clear channel of scheduled notifications.
```csharp
AndroidNotificationController.RefreshChannel();
```
Or use this.

```csharp
AndroidNotificationController.RefreshChannel(bool);
```


Call this method before CreateNotification. This is necessary to activate notification channel.

This must be done once, unless you intend to clear created scheduled notifications.

Next.

This creates notification with default parameters.

```csharp
AndroidNotificationController.CreateNotification(DateTime fireTime);
```
Or use this.

```csharp
AndroidNotificationController.CreateNotification(string, string, string, string, DateTime);
```



### NotificationRepeater

Specify required parameters in repeaterModel to create scheduled recurring notifications.
```csharp
NotificationRepeater.SwitchNotifications(NotificationRepeaterModel repeaterModel)
```

This method allows you to understand whether the user has reached the halfway point of the intended notification schedule.
```csharp
bool IsNeedUpdateNotifications(string midterm)
```

I use middle of dates as a point to clear and recreate repeating notifications.
