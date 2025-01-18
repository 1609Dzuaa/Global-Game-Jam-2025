using UnityEngine;

public class Rat : MonoBehaviour
{
    [SerializeField]
    private float speedX = 1f;

    [SerializeField]
    private Transform _pipeCheck;

    [SerializeField]
    float _pipeCheckDistance;

    [SerializeField]
    LayerMask _pipeLayer;

    private const string BUBBLE_TAG = "Bubble";

    private Rigidbody2D _rigidbody;
    bool _pipeDetected, _isRight = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag(BUBBLE_TAG))
            return;

        Destroy(other.gameObject);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveLogic();
    }

    private void MoveLogic()
    {
        if (!_pipeDetected)
        {
            _isRight = !_isRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2((_isRight) ? speedX : -speedX, _rigidbody.velocity.y);
    }


    private void FixedUpdate()
    {
        Move();
        PipeCheck();
    }

    private void PipeCheck()
    {
        _pipeDetected = Physics2D.Raycast(_pipeCheck.position, Vector2.down, _pipeCheckDistance, _pipeLayer);
        //Debug.Log("check: " + _pipeDetected);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_pipeCheck.position, _pipeCheck.position - new Vector3(0f, _pipeCheckDistance, 0f));
    }
}
