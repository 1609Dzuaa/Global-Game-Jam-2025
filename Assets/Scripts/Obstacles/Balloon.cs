using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Balloon : MonoBehaviour, IClickable
{
    [SerializeField]
    private Transform pointingDirection;

    private const string BUBBLE_TAG = "Bubble";
    private Vector3 direction;

    [SerializeField]
    private float pushForce = 10f;

    [SerializeField]
    private Collider2D _blowCollider;

    [SerializeField]
    private float cooldown = 0.5f;

    public UnityEvent onBalloonClicked;
    private float _timeSinceLastClick = Mathf.Infinity;
    private Animator[] _animators;

    private int _blowHash = Animator.StringToHash("blow");

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(BUBBLE_TAG))
            return;

        var rb = other.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * pushForce, ForceMode2D.Force);

        _blowCollider.enabled = false;
    }

    public void HandleClick()
    {
        if (_timeSinceLastClick < cooldown)
            return;

        if (_blowCollider.enabled)
            return;

        onBalloonClicked?.Invoke();
    }

    private void Awake()
    {
        direction = pointingDirection.position - transform.position;
        _blowCollider.enabled = false;
    }

    private void Start()
    {
        _animators = GetComponentsInChildren<Animator>();
    }

    private void Update()
    {
        _timeSinceLastClick += Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (_blowCollider.enabled)
        {
            _blowCollider.enabled = false;
        }
    }

    public void BlowAir()
    {
        Debug.Log("Blowing air");
        _blowCollider.enabled = true;

        foreach (var animator in _animators)
        {
            animator.SetTrigger(_blowHash);
        }

        _timeSinceLastClick = 0;
    }
}
