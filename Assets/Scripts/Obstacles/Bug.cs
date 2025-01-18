using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField]
    private float speedY = 1f;

    [SerializeField]
    private float speedX = 1f;

    [SerializeField]
    private float threshold = 2f;

    private const string BUBBLE_TAG = "Bubble";

    private Rigidbody2D _rigidbody;
    private Camera _cam;

    private float _currentAngle = 0f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag(BUBBLE_TAG))
            return;

        var bubble = other.gameObject.GetComponent<Bubble>();
        bubble.PopBubble(bubble);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _cam = Camera.main;
    }

    private void FixedUpdate()
    {
        _currentAngle += Time.fixedDeltaTime * speedY;
        var yPos = threshold * Mathf.Sin(_currentAngle);

        _rigidbody.velocity = new Vector2(
            -1 * speedX * Time.fixedDeltaTime,
            yPos * Time.fixedDeltaTime
        );

        CheckOutOfScreen();
    }

    private void CheckOutOfScreen()
    {
        if (transform.position.x < _cam.ScreenToWorldPoint(Vector3.zero).x)
            Destroy(gameObject);
    }
}
