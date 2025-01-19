using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public class PanelStory : MonoBehaviour
{
    [SerializeField] private float moveDuration;
    [SerializeField] private float fadeDuration;
    [SerializeField] private Image story1;
    [SerializeField] private Image story2;
    [SerializeField] private Image story2Faucet;
    [SerializeField] private Image story3;

    private void OnEnable()
    {
        DoAnimStory1(() =>
        {
            DoAnimStory2(() =>
            {
                DoAnimStory3(() =>
                {
                    SceneManager.LoadScene("GamePlayScene");
                    //UIManager.Instance.ShowView(PanelName.PanelForce);
                });
            });
        });
    }

    private void DoAnimStory1(Action callBack = null)
    {
        story1.transform.DOMove(Vector3.zero, moveDuration).OnComplete(() =>
        {
            DOVirtual.DelayedCall(2f, () =>
            {
                story1.DOColor(new Color32(50, 50, 50, 255), fadeDuration).OnComplete(() => callBack?.Invoke());
            });
        });
    }

    private void DoAnimStory2(Action callBack = null)
    {
        story2.transform.DOMove(Vector3.zero, moveDuration).OnComplete(() =>
        {
            story2Faucet.transform.DOScale(Vector3.one, 1).OnComplete(() =>
            {
                DOVirtual.DelayedCall(1f, () =>
                {
                    story2Faucet.DOColor(new Color32(50, 50, 50, 255), fadeDuration);
                    story2.DOColor(new Color32(50, 50, 50, 255), fadeDuration).OnComplete(() => callBack?.Invoke());
                });
            });
        });
    }

    private void DoAnimStory3(Action callBack = null)
    {
        story3.transform.DOMove(Vector3.zero, moveDuration).OnComplete(() =>
        {

            DOVirtual.DelayedCall(3f, () => callBack?.Invoke());
        });
    }
}
