using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTimer : MonoBehaviour
{
    [SerializeField] Image _imageForce;

    private void Start()
    {
        EventsManager.Subcribe(EventID.OnSendSliderTimer, HandleImage);
    }

    private void OnDestroy()
    {
        EventsManager.Unsubcribe(EventID.OnSendSliderTimer, HandleImage);
    }

    private void HandleImage(object obj)
    {
        float value = (float)obj;
        _imageForce.fillAmount = value;
    }
}
