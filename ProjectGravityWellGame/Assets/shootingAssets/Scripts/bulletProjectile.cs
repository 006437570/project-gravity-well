using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletProjectile : MonoBehaviour
{

    public float speed = 10f;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed; //When player shoots bullet travels right
    }

    void OnTriggerEnter2D (Collider2D hitDetection)
    {
        Debug.Log(hitDetection.name); //
        Destroy(gameObject); //Despawns bullet when colliding with a gameObject
    }
}
