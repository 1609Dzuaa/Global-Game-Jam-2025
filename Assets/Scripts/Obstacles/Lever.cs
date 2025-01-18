using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Lever : MonoBehaviour, IClickable
{
    [SerializeField]
    private bool _isOnlyTriggerByBubble;

    [SerializeField]
    private Platform[] _platforms;

    [SerializeField]
    private float _duration = 1f;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Sprite _spriteOn;

    [SerializeField]
    private Sprite _spriteOff;

    private const string BUBBLE_TAG = "Bubble";
    private bool _isReady = true;
    private bool _toggle;
    private List<Vector3> _platformsOriginalRotation = new List<Vector3>();

    private void Start()
    {
        for (int i = 0; i < _platforms.Length; i++)
        {
            _platformsOriginalRotation.Add(_platforms[i].platform.transform.rotation.eulerAngles);
        }
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
        if (!_isReady)
            return;
        _isReady = false;

        _toggle = !_toggle;

        if (_toggle)
        {
            for (int i = 0; i < _platforms.Length; i++)
            {
                if (i != _platforms.Length - 1)
                {
                    _platforms[i]
                        .platform.transform.DORotate(
                            new Vector3(0, 0, _platforms[i].rotateDegree),
                            _duration
                        );
                }
                else
                {
                    _platforms[i]
                        .platform.transform.DORotate(
                            new Vector3(0, 0, _platforms[i].rotateDegree),
                            _duration
                        )
                        .OnComplete(() => _isReady = true);
                }
            }
        }
        else
        {
            for (int i = 0; i < _platforms.Length; i++)
            {
                if (i != _platforms.Length - 1)
                {
                    _platforms[i]
                        .platform.transform.DORotate(
                            new Vector3(0, 0, _platformsOriginalRotation[i].z),
                            _duration
                        );
                }
                else
                {
                    _platforms[i]
                        .platform.transform.DORotate(
                            new Vector3(0, 0, _platformsOriginalRotation[i].z),
                            _duration
                        )
                        .OnComplete(() => _isReady = true);
                }
            }
        }

        _spriteRenderer.sprite = _toggle ? _spriteOn : _spriteOff;
    }

    [Serializable]
    public struct Platform
    {
        public GameObject platform;
        public float rotateDegree;
    }
}
