using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class WaterRise : MonoBehaviour
{
    [SerializeField]
    private Transform transformWater;

    Vector3 _startPos;

    private void KillTween(object obj)
    {
        transform.DOKill();

        transform.position = _startPos;
    }

    private void Awake()
    {
        _startPos = transform.position;

        var limitedTime = GameData.Instance.GetCurrentLevelConfig().limitedTime;
        transform.DOMoveY(this.transform.position.y + 1, limitedTime);

        EventsManager.Subcribe(EventID.OnLevelPassed, KillTween);
    }

    private void OnDestroy()
    {
        EventsManager.Unsubcribe(EventID.OnLevelPassed, KillTween);
    }
}
