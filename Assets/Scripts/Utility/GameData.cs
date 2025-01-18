using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static GameData _instante;
    public static GameData Instante;

    public int Level = 1;

    private void Awake()
    {
        if (Instante == null) Instante = this;
        else Destroy(this.gameObject);
    }
}
