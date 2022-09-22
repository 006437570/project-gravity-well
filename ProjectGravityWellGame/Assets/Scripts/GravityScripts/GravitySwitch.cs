using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System; //Allows use of absolute value math function

public class GravitySwitch : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If hits background with GravN tag normalize Gravity
        if (collision.gameObject.CompareTag("GravN"))
        {
            Debug.Log("Normalize!");
            rb.gravityScale = Math.Abs(rb.gravityScale);
        }
        // if hit background with GravR tag reverse Gravity
        if (collision.gameObject.CompareTag("GravR"))
        {
            Debug.Log("Reverse!");
            rb.gravityScale = -rb.gravityScale;
        }
    }

}

/* OLD CODE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    private Rigidbody2D rgdBdy;

    private void Awake()
    {
        rgdBdy = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GravSw"))
        {
            Debug.Log("Switch!");
            rgdBdy.gravityScale *= -1;

        }
    }
}
*/