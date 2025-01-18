using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    [SerializeField] Image _imageLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayOnClick()
    {
        SceneManager.LoadScene(GameData.Instance.GetCurrentLevelConfig().level);
    }
}
