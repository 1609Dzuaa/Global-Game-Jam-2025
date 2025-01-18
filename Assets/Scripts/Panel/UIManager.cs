using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class UIManager : MonoBehaviour
{
    [SerializeField] private PanelSO _config;
    private Dictionary<PanelName, GameObject> dictionary = new();

    private void Awake()
    {
        InitUI();
    }

    private void InitUI()
    {
        foreach (var item in _config.Panels)
        {
            // dictionary[item.panelName] = new();
            var newGameObject = GameObject.Instantiate(item.prefab, this.transform);
            newGameObject.SetActive(false);
            dictionary.Add(item.panelName, newGameObject);
        }
    }

    public void ShowView(PanelName panelName)
    {
        var result = dictionary.FirstOrDefault(p => p.Key == panelName).Value;
        if (result != null) result.SetActive(true);
    }

    public void HideView(PanelName panelName)
    {
        var result = dictionary.FirstOrDefault(p => p.Key == panelName).Value;
        if (result != null) result.SetActive(false);
    }
}