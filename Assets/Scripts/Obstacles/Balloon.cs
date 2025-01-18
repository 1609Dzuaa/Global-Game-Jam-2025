using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent OnBalloonClicked;
    private float _timeSinceLastClick = Mathf.Infinity;

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

        OnBalloonClicked?.Invoke();
    }

    private void Awake()
    {
        direction = pointingDirection.position - transform.position;
        _blowCollider.enabled = false;
    }

    private void Update()
    {
        _timeSinceLastClick += Time.deltaTime;
    }

    public void BlowAir()
    {
        Debug.Log("Blowing air");
        _blowCollider.enabled = true;
    }
}
