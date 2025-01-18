using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelMainMenu : MonoBehaviour
{
    [SerializeField] private Button _btnStart;
    [SerializeField] private Button _btnOption;
    [SerializeField] private Button _btnExit;

    const int FIRST_LEVEL = 1;

    private void OnEnable()
    {
        _btnStart.onClick.AddListener(OnClickedStart);
        _btnOption.onClick.AddListener(OnClickedOption);
        _btnExit.onClick.AddListener(OnClickedExit);
    }

    private void OnDisable()
    {
        _btnStart.onClick.RemoveAllListeners();
        _btnOption.onClick.RemoveAllListeners();
        _btnExit.onClick.RemoveAllListeners();
    }

    private void OnClickedStart()
    {
        SceneManager.LoadScene(FIRST_LEVEL);
    }

    private void OnClickedOption()
    {

    }

    private void OnClickedExit()
    {

    }
}
