using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelWin : MonoBehaviour
{
    LevelLoader _levelLoader;

    // Start is called before the first frame update
    void Awake()
    {
        EventsManager.Subcribe(EventID.OnLevelPassed, ShowPanel);
    }

    void Start()
    {
        _levelLoader = FindObjectOfType<LevelLoader>();
    }

    private void ShowPanel(object obj)
    {
        Debug.Log("Show panel win");
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        EventsManager.Unsubcribe(EventID.OnLevelPassed, ShowPanel);
    }

    public void OnContinueClick()
    {
        var nextLevel = GameData.Instance.GetCurrentLevelConfig().level + 1;
        if (!GameData.Instance.SetLevel(nextLevel))
        {
            gameObject.SetActive(false);
            return;
        }

        var config = GameData.Instance.GetCurrentLevelConfig();
        _levelLoader.LoadLevel(config);
        gameObject.SetActive(false);
    }
}
