using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pool : MonoBehaviour
{
    private static Pool _instante;
    public static Pool Instante { get; set; }
    [SerializeField] private PoolSO poolPrefab;
    private Dictionary<string, List<GameObject>> dict = new();
    private void Awake()
    {
        if (Instante == null) Instante = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        StartCoroutine(PopulatePrefab());
    }

    private IEnumerator PopulatePrefab()
    {
        yield return null;
        foreach (var prefab in poolPrefab.prefabs)
        {
            dict[prefab.name] = new();
            var gameObj = GameObject.Instantiate(prefab, this.transform);
            if (gameObj != null)
            {
                dict[prefab.name].Add(gameObj);
                gameObj.SetActive(false);
            }
        }
    }

    public GameObject Spawn(string name, Vector2 position)
    {
        if (!dict.ContainsKey(name)) return null;

        var result = dict[name].FirstOrDefault(p => p != null && !p.activeSelf);
        if(result == null)
        {
            result = GameObject.Instantiate(poolPrefab.prefabs.FirstOrDefault(p => p.name == name), transform);
            dict[name].Add(result);
        }
        result.transform.position = position;
        result.SetActive(true);
        return result;
    }
}
