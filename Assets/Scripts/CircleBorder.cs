using UnityEngine;

public class CircleBorder : MonoBehaviour
{
    [SerializeField]
    private float radius = 1f;

    private Vector3 _scale;

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius * _scale.x);
    }

    public Vector3 GetPositionOnCircle(float angle)
    {
        var center = transform.position;

        var position =
            center
            + new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius * _scale.x,
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius * _scale.y,
                0
            );

        return position;
    }

    private void Awake()
    {
        _scale = transform.localScale;
    }
}
