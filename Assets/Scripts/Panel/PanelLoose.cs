using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelLoose : MonoBehaviour
{
    private void Awake()
    {
        EventsManager.Subcribe(EventID.OnLevelFailed, ShowPanel);
    }

    private void OnDestroy()
    {
        EventsManager.Unsubcribe(EventID.OnLevelFailed, ShowPanel);
    }

    private void ShowPanel(object obj) => gameObject.SetActive(true);

    public void OnContinueClick()
    {
        SceneManager.LoadScene("GamePlayScene");
        gameObject.SetActive(false);
    }
}
