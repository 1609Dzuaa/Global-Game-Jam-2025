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
        GameData.Instante.SetLevel(1); //Jusg to test, set level in select level
        SceneManager.LoadScene("SampleScene");
    }

    private void OnClickedOption()
    {

    }

    private void OnClickedExit()
    {

    }
}
