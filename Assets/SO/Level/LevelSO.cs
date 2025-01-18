using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LevelSO", menuName = "SO/Level")]
public class LevelSO : ScriptableObject
{
    public List<LevelConfig> levelConfigs;
}

[Serializable]
public class LevelConfig
{
    public int level;
    public GameObject tileMap;
    public Sprite background;
}
