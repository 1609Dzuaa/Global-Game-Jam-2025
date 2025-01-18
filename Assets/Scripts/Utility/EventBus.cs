using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public static class EventsManager
{
    static Dictionary<EventID, Action<object>> _dictEvents =
        new Dictionary<EventID, Action<object>>();

    public static void Subcribe(EventID eventID, Action<object> callback)
    {
        if (!_dictEvents.ContainsKey(eventID))
        {
            _dictEvents.Add(eventID, callback);
            return;
        }

        _dictEvents[eventID] += callback;
    }

    public static void Unsubcribe(EventID eventID, Action<object> callback)
    {
        if (_dictEvents.ContainsKey(eventID))
            _dictEvents[eventID] -= callback;
    }

    public static void Notify(EventID eventID, object eventArgs = null)
    {
        if (!_dictEvents.ContainsKey(eventID))
            return;
        _dictEvents[eventID]?.Invoke(eventArgs);
    }
}
