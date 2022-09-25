using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletProjectile : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb; //gives bullet rigidbody to give it physics

    void Start()
    {
        rb.velocity = transform.right * speed; //When player shoots bullet travels right
    }

    void OnTriggerEnter2D (Collider2D hitDetection)
    {
        Debug.Log(hitDetection.name); // Prints what projectile hit in debug console
        
        //Temp Code
        //Change to if hit player or wall (destroy) if player do damage
        if (hitDetection.gameObject.CompareTag("GravN") || hitDetection.gameObject.CompareTag("GravR"))
        {
        }
        else
        {
        Destroy(gameObject); //Despawns bullet when colliding with a gameObject
        }
    }
}
