using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lever : MonoBehaviour, IClickable
{
    [SerializeField]
    private bool _isOnlyTriggerByBubble;

    [SerializeField]
    private Transform _platform;

    [SerializeField]
    private Transform _point;

    [SerializeField]
    private float _duration = 1f;

    private const string BUBBLE_TAG = "Bubble";
    private bool _isReady = true;
    private bool _toggle;
    private Vector2 _cachedPosition;

    private void OnEnable()
    {
        _cachedPosition = _platform.position;
        _isReady = true;
    }
    public void HandleClick()
    {
        if (_isOnlyTriggerByBubble) 
            return;
        
        LeverTrigger();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag(BUBBLE_TAG))
            return;

        LeverTrigger();
    }

    private void LeverTrigger()
    {
        if (!_isReady) return;
        _isReady = false;

        _toggle = !_toggle;
        if (_toggle) _platform.DOMove(new Vector2(_point.position.x, _point.position.y), _duration).OnComplete(() => _isReady = true);
        else _platform.DOMove(_cachedPosition, _duration).OnComplete(() => _isReady = true);
    }
}
