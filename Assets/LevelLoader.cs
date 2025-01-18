using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelSO config;

    private void Awake()
    {
        var data = config.levelConfigs.FirstOrDefault(p => p.level == GameData.Instante.Level);
        if(data == null)
        {
            Debug.LogError("Null level data");
            return;
        }

        GameObject.Instantiate(data.tileMap, Vector3.zero, Quaternion.identity, transform);
        //set background
    }
}
