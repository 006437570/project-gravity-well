using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// will be merged into fire projectileGun script
public class bulletProjectile : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb; //gives bullet rigidbody to give it physics
    public int dmg = 1;

    // Game Object for animation that plays when bullet hits something
    // public GameObject hitDetectionAni;

    void Start()
    {
        rb.velocity = transform.right * speed; //When player shoots bullet travels right
    }

    //find a way to place this into new hitDetection script?
    void OnTriggerEnter2D (Collider2D hitDetection)
    {
        PlayerHealth player = hitDetection.GetComponent<PlayerHealth>();
        
        if (player != null)
        {
            player.calcDmg(dmg);
            Destroy(gameObject);
        }
        if (hitDetection.gameObject.CompareTag("Terrain") || (hitDetection.gameObject.CompareTag("Projectile")) )
        {
            Destroy(gameObject);
        }

        //add feature to play hitDetectionAni when in contact
    }
}
