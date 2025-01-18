using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameData : MonoBehaviour
{
    private static GameData _instante;
    public static GameData Instante;

    [SerializeField] private LevelSO _configLevel;
    private LevelConfig _currentLevel;

    private void Awake()
    {
        if (Instante == null) Instante = this;
        else Destroy(this.gameObject);
    }

    public void SetLevel(int level)
    {
        _currentLevel = _configLevel.levelConfigs.FirstOrDefault(p => p.level == level);
    }

    public LevelConfig GetCurrentLevelConfig()
    {
        return _currentLevel;
    }
}
