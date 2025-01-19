using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class BubbleGunController : MonoBehaviour
{
    [SerializeField]
    Bubble _bubblePrefab;

    [SerializeField]
    Transform _spawnPositionY;

    [SerializeField]
    float scaleFactor = 10f;

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
    bool _isInCoroutine = false;
    bool _startInput = false;

    public bool HasSpawn { get; private set; } = false;

    const float DEFAULT_VALUE_ZERO = 0.0f;

    private void Start()
    {
        UIManager.Instance.ShowView(PanelName.PanelForce);
    }

    private void Update()
    {
        if (HasSpawn)
            return;

        if (Input.GetMouseButton(0) && !_isInCoroutine)
        {
            StartCoroutine(HandleTouchHold());
        }

        if (_startInput)
        {
            HandleSpawnBubble();
            HandleScaleBubbleAndForce();
        }
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
                _endValue = Mathf.Clamp(
                    _endValue + ((_isUp) ? _duration : -_duration),
                    0,
                    _maxForceTime
                );
                _endValue = Mathf.Round(_endValue / _duration) * _duration;

                if (!_isMaxScale)
                    _bubbleInstantiated.transform.DOScale(_endValue, _duration);

                _timerEach = Time.time;
                EventsManager.Notify(EventID.OnSendSliderForce, _endValue / _maxForceTime);
                //Debug.Log("scale bubble: " + _endValue / _maxForceTime);
            }

            //Debug.Log("still run");
        }
    }

    private void HandleSpawnBubble()
    {
        if (Input.touchCount > 0 && !_hasHold)
        {
            SpawnBubble();
            _hasHold = true;
            _holdTimer = _timerEach = Time.time;
            _endValue = DEFAULT_VALUE_ZERO;
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !HasSpawn)
        {
            _bubbleInstantiated.IsRealeased = true;
            HasSpawn = true;

            var rigidbody2D = _bubbleInstantiated.GetComponent<Rigidbody2D>();
            var force = Vector2.up * (scaleFactor * _bubbleInstantiated.transform.localScale.x);
            Debug.Log("force: " + force);

            rigidbody2D.AddForce(force, ForceMode2D.Impulse);

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
        EventsManager.Notify(EventID.OnGameStart);
    }

    private IEnumerator HandleTouchHold()
    {
        _isInCoroutine = true;
        yield return new WaitForSeconds(1f);
        _startInput = true;

        _isInCoroutine = false;
    }
}
