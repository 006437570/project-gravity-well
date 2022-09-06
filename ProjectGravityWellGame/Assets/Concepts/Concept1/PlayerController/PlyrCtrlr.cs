using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyrCtrlr : MonoBehaviour
{
    private Rigidbody2D plyrRgdBdy;

    //Player Movement
    public float plyrSpd;

    //Jump Dir
    private float jmpDir;

    void Awake()
    {
        plyrRgdBdy = GetComponent<Rigidbody2D>();
        jmpDir = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        plyrRgdBdy.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * plyrSpd, plyrRgdBdy.velocity.y);

        //Jump
        if(Input.GetKey(KeyCode.Space))
        {
            plyrRgdBdy.velocity = new Vector2(plyrRgdBdy.velocity.x * jmpDir, plyrSpd) * jmpDir;
        }

    }

    //On touch line, switch gravity
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GravSw"))
        {
            Debug.Log("Switch!");
            jmpDir *= -1;
            plyrRgdBdy.gravityScale *= -1;
            
        }
    }

}
