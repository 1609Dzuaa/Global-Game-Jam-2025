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
    TMPro.TextMeshProUGUI redImageText;

    [SerializeField]
    float duration = 40f;

    private void Start()
    {
        blueImage.SetActive(true);
        redImage.SetActive(false);

        HandleImage();
    }

    private void HandleImage()
    {
        TextMeshProUGUI timerText = blueImageText;
        float timeRemaining = duration;

        float elapsedTime = 0f;

        DOTween
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

                if (timeRemaining / duration < 0.3f)
                {
                    blueImage.SetActive(true);
                    redImage.SetActive(false);

                    timerText = redImageText;
                }
                else
                {
                    blueImage.SetActive(true);
                    redImage.SetActive(false);
                }
            })
            .OnComplete(() =>
            {
                EventsManager.Notify(EventID.OnTimerEnds, null);
            });
    }
}
