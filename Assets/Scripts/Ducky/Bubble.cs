using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField]
    private float scaleForce = 1f;

    [SerializeField]
    private float smallScalePop = 0.03f;

    [SerializeField]
    private Bubble bubblePrefab;

    private Rigidbody2D _rigidbody2D;
    private CircleBorder _circleBorder;

    private float _elapsedTime = 0f;
    private int _popAnimationHash = Animator.StringToHash("pop");

    public bool IsRealeased { get; set; }
    private bool _isPopped = false;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _circleBorder = GetComponent<CircleBorder>();
        _rigidbody2D.mass = Mathf.Clamp(transform.localScale.x, 0.5f, 1.5f);
    }

    private void FixedUpdate()
    {
        if (!IsRealeased || _isPopped)
        {
            PreventMoving();
            return;
        }
        else
        {
            Realeased();
        }

        var upForce = new Vector2(0, (1 * scaleForce * Time.fixedDeltaTime));
        _rigidbody2D.AddForce(upForce, ForceMode2D.Impulse);

        _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, 1.5f);
    }

    private void PreventMoving()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0f;
        _rigidbody2D.gravityScale = 0f;
    }

    private void Realeased()
    {
        _rigidbody2D.gravityScale = 1f;
    }

    public void SeparateBubble()
    {
        if (_isPopped)
            return;

        // Check if the bubble is too small to separate
        var newScale = transform.localScale / 2;
        if (newScale.x < smallScalePop)
        {
            PopBubble(this);
            return;
        }

        // Get positions for the two bubbles
        var bubble1Pos = _circleBorder.GetPositionOnCircle(0) * 0.8f; // Slightly inward
        var bubble2Pos = _circleBorder.GetPositionOnCircle(180) * 0.8f; // Slightly inward

        // Instantiate new bubbles
        var bubble1 = Instantiate(bubblePrefab, bubble1Pos, Quaternion.identity);
        var bubble2 = Instantiate(bubblePrefab, bubble2Pos, Quaternion.identity);

        // Release the new bubbles
        bubble1.IsRealeased = true;
        bubble2.IsRealeased = true;

        // Scale the new bubbles
        bubble1.transform.localScale = newScale;
        bubble2.transform.localScale = newScale;

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

        PopBubble(this);
    }

    public void PopBubble(Bubble bubble)
    {
        if (_isPopped)
            return;

        bubble.GetComponentInChildren<Animator>().SetTrigger(_popAnimationHash);
        bubble.GetComponent<Collider2D>().enabled = false;
        _isPopped = true;

        Destroy(bubble.gameObject, 2f);
    }
}
