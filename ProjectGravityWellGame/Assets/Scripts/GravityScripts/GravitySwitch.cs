using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Probably switch back to gravSwitch
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if hit background with GravR tag reverse Gravity
        if (collision.gameObject.CompareTag("GravSw"))
        {
            rb.gravityScale = -rb.gravityScale;
        }
    }
}