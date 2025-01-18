using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelLoader : MonoBehaviour
{
    private void Awake()
    {
        var _currentLevel = GameData.Instante.GetCurrentLevelConfig();
        if(_currentLevel == null)
        {
            Debug.LogError("Null level data");
            return;
        }
        GameObject.Instantiate(_currentLevel.tileMap, Vector3.zero, Quaternion.identity, transform);
        //set background
    }
}
