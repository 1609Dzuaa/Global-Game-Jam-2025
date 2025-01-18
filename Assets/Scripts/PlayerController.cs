using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerAnim _animation;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleRaycast();
            return;
        }
    }

    private static void HandleRaycast()
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
}