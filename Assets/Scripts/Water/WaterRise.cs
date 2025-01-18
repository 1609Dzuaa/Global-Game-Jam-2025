using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class WaterRise : MonoBehaviour
{
    [SerializeField] private Transform transformWater;

    private void Awake()
    {
        var limitedTime = GameData.Instance.GetCurrentLevelConfig().limitedTime;
        transform.DOMoveY(this.transform.position.y + 1, limitedTime);
    }
}
