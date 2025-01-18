using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoReturnToPool : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 2;
    private float _time;

    private void OnEnable()
    {
        _time = 0;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if(_time > _lifeTime)
        {
            this.gameObject.SetActive(false);
        }
    }
}
