using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BubbleGunController : MonoBehaviour
{
    [SerializeField]
    Bubble _bubblePrefab;

    [SerializeField]
    Transform _spawnPositionY;

    [SerializeField]
    float _maxForceTime,
        _duration;
    bool _hasHold = false,
        _isUp = true,
        _isMaxScale = false;
    float _holdTimer,
        _timerEach,
        _endValue;
    Bubble _bubbleInstantiated;

    public bool HasSpawn { get; private set; } = false;

    const float DEFAULT_VALUE_ZERO = 0.0f;

    private void Update()
    {
        HandleSpawnBubble();
        HandleScaleBubbleAndForce();
    }

    private void HandleScaleBubbleAndForce()
    {
        if (_bubbleInstantiated != null)
        {
            if (Time.time - _holdTimer >= _maxForceTime)
            {
                _isUp = !_isUp;
                if (!_isMaxScale)
                    _isMaxScale = true;
                _holdTimer = Time.time;
            }

            if (Time.time - _timerEach > _duration && Input.GetMouseButton(0))
            {
                _endValue = _endValue + ((_isUp) ? _duration : -_duration);
                if (!_isMaxScale)
                    _bubbleInstantiated.transform.DOScale(_endValue, _duration);
                _timerEach = Time.time;
                // EventsManager.Notify(EventID.OnSendSliderForce, _endValue / _maxForceTime);
                Debug.Log("scale bubble" + _endValue / _maxForceTime);
            }

            Debug.Log("still run");
        }
    }

    private void HandleSpawnBubble()
    {
        if (Input.GetMouseButton(0) && !_hasHold)
        {
            SpawnBubble();
            _hasHold = true;
            _holdTimer = _timerEach = Time.time;
            _endValue = DEFAULT_VALUE_ZERO;
        }
        else if (Input.GetMouseButtonUp(0) && !HasSpawn)
        {
            _bubbleInstantiated.IsRealeased = true;
            HasSpawn = true;

            //thả chuột, cấp lực cho bubble bay lên
            //SpawnBubble();
        }
        else if (_hasHold && !HasSpawn && Time.time - _holdTimer >= _maxForceTime)
        {
            //Reset thhanh lực
            // -= _duration;
            //EventsManager.Notify(EventID.OnSendSliderForce, _endValue / _maxForceTime);
        }
    }

    private void SpawnBubble()
    {
        Vector3 shootPos = new Vector3(transform.position.x, _spawnPositionY.position.y, 0f);
        _bubbleInstantiated = Instantiate(_bubblePrefab, shootPos, Quaternion.identity);
        _bubbleInstantiated.transform.localScale = new Vector3(
            DEFAULT_VALUE_ZERO,
            DEFAULT_VALUE_ZERO,
            DEFAULT_VALUE_ZERO
        );
    }
}
