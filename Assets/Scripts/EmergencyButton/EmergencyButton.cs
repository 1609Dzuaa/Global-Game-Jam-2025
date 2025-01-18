using System;
using UnityEngine;

public class EmergencyButton : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        EventsManager.Notify(EventID.OnLevelPassed);
    }
}
