using System;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField]
    private Transform pointingDirection;

    private const string BUBBLE_TAG = "Bubble";
    private Vector3 direction;

    [SerializeField]
    private float pushForce = 10f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag(BUBBLE_TAG))
            return;

        var rb = other.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * pushForce, ForceMode2D.Force);
    }

    private void Awake()
    {
        direction = pointingDirection.position - transform.position;
    }
}
