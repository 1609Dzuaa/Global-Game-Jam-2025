using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelTimer : MonoBehaviour
{
    [SerializeField]
    GameObject blueImage;

    [SerializeField]
    TMPro.TextMeshProUGUI blueImageText;

    [SerializeField]
    GameObject redImage;

    [SerializeField]
    TextMeshProUGUI redImageText;

    private TextMeshProUGUI timerText;
    private float duration;
    private float timeRemaining;
    private Tween tween;

    private void Start()
    {
        blueImage.SetActive(true);
        redImage.SetActive(false);
        HandleImage();

        EventsManager.Subcribe(EventID.OnLevelPassed, KillTween);
    }

    private void KillTween(object obj)
    {
        tween.Kill();
        HandleImage();
    }

    private void SetUp()
    {
        duration = GameData
            .Instance.GetLevelConfig(GameData.Instance.GetCurrentLevelConfig().level + 1)
            .limitedTime;
        timeRemaining = duration;
        timerText = blueImageText;
        timerText.text =
            Mathf.FloorToInt(timeRemaining / 60).ToString("00")
            + ":"
            + Mathf.CeilToInt(timeRemaining % 60).ToString("00");
    }

    private void HandleImage()
    {
        SetUp();
        float elapsedTime = 0f;

        tween = DOTween
            .To(() => timeRemaining, x => timeRemaining = x, 0f, duration)
            .OnUpdate(() =>
            {
                // Update elapsed time with deltaTime
                elapsedTime += Time.deltaTime;
                timeRemaining = Mathf.Max(0, duration - elapsedTime);

                // Update the timer text
                if (timeRemaining >= 60)
                {
                    timerText.text =
                        Mathf.FloorToInt(timeRemaining / 60).ToString("00")
                        + ":"
                        + Mathf.CeilToInt(timeRemaining % 60).ToString("00");
                }
                else
                {
                    timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
                }

                if (timeRemaining / duration > 0.3f)
                {
                    blueImage.SetActive(true);
                    redImage.SetActive(false);
                }
                else
                {
                    blueImage.SetActive(false);
                    redImage.SetActive(true);

                    timerText = redImageText;
                }
            })
            .OnComplete(() =>
            {
                UIManager.Instance.HideView(PanelName.PanelTimer);
                UIManager.Instance.HideView(PanelName.PanelForce);
                EventsManager.Notify(EventID.OnLevelFailed);
            });
    }
}
