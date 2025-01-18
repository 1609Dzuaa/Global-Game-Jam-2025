using UnityEngine;

public class Spike : MonoBehaviour
{
    private const string BUBBLE_TAG = "Bubble";

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag(BUBBLE_TAG))
            return;

        Destroy(other.gameObject);
    }
}
