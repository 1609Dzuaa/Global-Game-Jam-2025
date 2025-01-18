using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerAnim _animation;

    [SerializeField]
    private BubbleGunController _bubbleGunController;

    [SerializeField]
    private PlayerMover _playerMover;

    [SerializeField]
    private float offsetOnPlayer = 0.5f;

    private Vector3 _mousePosition;

    private void Update()
    {
        if (!_bubbleGunController.HasSpawn)
        {
            if (Input.GetMouseButton(0))
            {
                _playerMover.StopMove();
                _animation.SetAnim(AnimationName.Blow);
            }
            else if (!IsMouseOverPlayerX())
            {
                _animation.SetAnim(AnimationName.Move);
                _playerMover.Move(_mousePosition);
            }
            else
            {
                _animation.SetAnim(AnimationName.Idle);
            }
        }
        // else
        // {
        if (Input.GetMouseButtonDown(0))
        {
            HandleRaycast();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleBubbleSplit();
        }

        if (!IsMouseOverPlayerX())
        {
            _animation.SetAnim(AnimationName.Move);
            _playerMover.Move(_mousePosition);
        }
        else
        {
            _animation.SetAnim(AnimationName.Idle);
        }
        // }
    }

    private void HandleRaycast()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hits = new List<RaycastHit2D>();
        hits = Physics2D
            .RaycastAll(mousePosition, Vector2.zero)
            .Where(hit => hit.collider.GetComponent<IClickable>() is not null)
            .ToList();
        if (hits.Count == 0)
            return;

        foreach (var clickable in hits.Select(hit => hit.collider.GetComponent<IClickable>()))
        {
            clickable.HandleClick();
        }
    }

    private void HandleMove() { }

    private void HandleBubbleSplit()
    {
        var bubbles = FindObjectsOfType<Bubble>();
        foreach (var bubble in bubbles)
        {
            bubble.SeparateBubble();
        }
    }

    private bool IsMouseOverPlayerX()
    {
        return false;

        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return _mousePosition.x > transform.position.x - offsetOnPlayer
            && _mousePosition.x < transform.position.x + offsetOnPlayer;
    }
}
