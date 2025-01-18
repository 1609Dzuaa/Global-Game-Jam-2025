using DG.Tweening;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float duration = 2f;

    [SerializeField]
    private float cameraBoundaryOffset = 0.2f;

    private Camera _camera;
    private Tween _moveAction;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void StopMove()
    {
        if (_moveAction is not null)
        {
            _moveAction.Kill();
        }
    }

    public void Move(Vector2 direction)
    {
        if (_moveAction is not null)
        {
            _moveAction.Kill();
        }

        var directionX = Mathf.Clamp(
            direction.x,
            _camera.ViewportToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane)).x
                + cameraBoundaryOffset,
            _camera.ViewportToWorldPoint(new Vector3(1, 0, _camera.nearClipPlane)).x
                - cameraBoundaryOffset
        );
        _moveAction = transform.DOMoveX(directionX, duration);
        FaceDirection(direction);
    }

    private void FaceDirection(Vector2 direction)
    {
        if (direction.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
