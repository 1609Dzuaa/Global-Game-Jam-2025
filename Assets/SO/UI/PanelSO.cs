using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIConfig", menuName = "SO/UI")]
public class PanelSO : ScriptableObject
{
    public List<PanelUI> Panels;
}

[Serializable]
public class PanelUI
{
    public PanelName panelName;
    public GameObject prefab;
}

public enum PanelName
{
    PanelMainMenu,
    PanelOption,
    PanelTimer,
    PanelTimer1,
}
