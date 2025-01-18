using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGunController : MonoBehaviour
{
    [SerializeField] Bubble _bubblePrefab;
    [SerializeField] Transform _spawnPositionY;
    [SerializeField] float _maxHoldingTime, _duration;
    bool _hasHold = false, _hasSpawn = false;
    float _holdTimer, _timerEach, _endValue;
    Bubble _bubbleInstantiated;

    const float DEFAULT_VALUE_ONE = 1.0f;

    private void Update()
    {
        HandleSpawnBubble();
        HandleScaleBubble();
    }

    private void HandleScaleBubble()
    {
        if (_bubbleInstantiated != null)
        {
            if (Time.time - _holdTimer >= _maxHoldingTime) return;

            if (Time.time - _timerEach > _duration && Input.GetMouseButton(0))
            {
                _endValue += _duration;
                _bubbleInstantiated.transform.DOScale(_endValue, _duration);
                _timerEach = Time.time;
                Debug.Log("scale bubble");
            }
        }
    }

    private void HandleSpawnBubble()
    {
        if (Input.GetMouseButton(0) && !_hasHold)
        {
            SpawnBubble();
            _hasHold = true;
            _holdTimer = _timerEach = Time.time;
            _endValue = DEFAULT_VALUE_ONE;
        }
        else if (Input.GetMouseButtonUp(0) && !_hasSpawn)
        {
            //SpawnBubble();
        }
        else if (_hasHold && !_hasSpawn && Time.time - _holdTimer >= _maxHoldingTime)
        {
            //SpawnBubble();
        }
    }

    private void SpawnBubble()
    {
        _hasSpawn = true;
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(
        mousePos.x,
        mousePos.y,
        Camera.main.nearClipPlane));

        Vector3 shootPos = new Vector3(worldPosition.x, _spawnPositionY.position.y, 0f);
        _bubbleInstantiated = Instantiate(_bubblePrefab, shootPos, Quaternion.identity);
    }
}
