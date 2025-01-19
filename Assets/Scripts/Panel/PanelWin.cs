using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelWin : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        EventsManager.Subcribe(EventID.OnLevelPassed, ShowPanel);    
    }

    private void ShowPanel(object obj) => gameObject.SetActive(true);

    private void OnDestroy()
    {
        EventsManager.Unsubcribe(EventID.OnLevelPassed, ShowPanel);
    }

    public void OnContinueClick()
    {
        GameData.Instance.SetLevel(2); //Jusg to test, set level in select level
        SceneManager.LoadScene("GamePlayScene");
    }
}
