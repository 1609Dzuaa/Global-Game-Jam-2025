using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField]
    private float scaleForce = 1f;

    [SerializeField]
    private float massScale = 1f;

    [SerializeField]
    private float timeToSeparate = 2f;

    [SerializeField]
    private float smallScalePop = 0.03f;

    [SerializeField]
    private Bubble bubblePrefab;

    private Rigidbody2D _rigidbody2D;
    private CircleBorder _circleBorder;

    private float _elapsedTime = 0f;
    private bool _isInitialized = false;

    public void Initialize()
    {
        if (transform.localScale.x <= smallScalePop)
        {
            Destroy(gameObject);
            return;
        }

        _rigidbody2D.mass = Mathf.Clamp(transform.localScale.x, 0.5f, 1.5f);

        _isInitialized = true;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _circleBorder = GetComponent<CircleBorder>();
    }

    private void Update()
    {
        if (!_isInitialized)
            Initialize();

        _elapsedTime += Time.deltaTime;

        if (!(_elapsedTime >= Random.Range(timeToSeparate, timeToSeparate + 1f)))
            return;
        SeparateBubble();
    }

    private void FixedUpdate()
    {
        var upForce = new Vector2(0, (1 * scaleForce * Time.fixedDeltaTime));
        _rigidbody2D.AddForce(upForce, ForceMode2D.Impulse);

        _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, 1.5f);
    }

    private void SeparateBubble()
    {
        // Get positions for the two bubbles
        var bubble1Pos = _circleBorder.GetPositionOnCircle(0) * 0.8f; // Slightly inward
        var bubble2Pos = _circleBorder.GetPositionOnCircle(180) * 0.8f; // Slightly inward

        // Instantiate new bubbles
        var bubble1 = Instantiate(bubblePrefab, bubble1Pos, Quaternion.identity);
        var bubble2 = Instantiate(bubblePrefab, bubble2Pos, Quaternion.identity);

        // Scale the new bubbles
        bubble1.transform.localScale = transform.localScale / 2;
        bubble2.transform.localScale = transform.localScale / 2;

        // Add a small separation force
        var forceMagnitude = 1f; // Adjust this value to control the separation speed
        var direction1 = (bubble1Pos - transform.position).normalized;
        var direction2 = (bubble2Pos - transform.position).normalized;

        bubble1
            .GetComponent<Rigidbody2D>()
            .AddForce(direction1 * forceMagnitude, ForceMode2D.Impulse);
        bubble2
            .GetComponent<Rigidbody2D>()
            .AddForce(direction2 * forceMagnitude, ForceMode2D.Impulse);

        Destroy(gameObject);
    }
}
