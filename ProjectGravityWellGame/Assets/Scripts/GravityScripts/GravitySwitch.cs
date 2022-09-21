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
