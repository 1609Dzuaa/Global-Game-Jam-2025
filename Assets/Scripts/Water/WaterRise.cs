using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class WaterRise : MonoBehaviour
{
    [SerializeField] private Transform transformWater;

    private void OnEnable()
    {
        EventsManager.Subcribe(EventID.OnGameStart, OnGameStart);
    }

    private void OnDisable()
    {
        EventsManager.Unsubcribe(EventID.OnGameStart, OnGameStart);
    }

    private void OnGameStart(object obj)
    {
        var limitedTime = GameData.Instante.GetCurrentLevelConfig().limitedTime;
        transform.DOMoveY(this.transform.position.y + 1, limitedTime);
    }
}
