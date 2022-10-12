using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigid;
    Vector2 input;
    public float speed;
    bool moving = false;

    float inputHorizontal;

    public float jumpSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    bool facingRight = true;

    //Damage
    /*
    PlayerHealth takeDmg;
    public int dmgAmount;*/

    // Start is called before the first frame update
    void Awake()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        /*takeDmg = GetComponent<PlayerHealth>();
        dmgAmount = 1;*/

    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        input = new Vector2(Input.GetAxis("Horizontal") * speed, playerRigid.velocity.y);

        playerRigid.AddForce(input * speed * Time.deltaTime);

        inputHorizontal = Input.GetAxisRaw("Horizontal");

        
        
                if (inputHorizontal > 0 && !facingRight)
                {
            Flip();
                }
                if (inputHorizontal < 0 && facingRight)
                {
            Flip();
        }
        


        moving = (input.x != 0 || input.y != 0);

        //Alternative Force: Attatches inputs to player object directly
        //playerRigid.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, playerRigid.velocity.y);
        //moving = (playerRigid.velocity.x != 0 || playerRigid.velocity.y != 0);

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            playerRigid.velocity = new Vector2(playerRigid.velocity.x, jumpSpeed);
        }

        /*
        //Test Damage
        if(Input.GetKeyDown(KeyCode.K))
        {
            takeDmg.calcDmg(dmgAmount);
            Debug.Log("Damage!");
        }
        */

    }

    void Flip()
    {
        /*
        Vector3 currentScale = gameObject.transform.localScale;

        currentScale.x *= -1;

        gameObject.transform.localScale = currentScale;
        */

        // the old code didn't rotate the direction of the gun shot
        // this does. idk how :P
        transform.Rotate(0f, 180f, 0f);

        facingRight = !facingRight;
    }
    /*
    public void FlipY(Rigidbody2D rb, Collision2D collision)
    {
        Vector3 currentScale = gameObject.transform.localScale;


        if (collision.gameObject.CompareTag("GravN"))
        {
            if (playerRigid.position.y < 0)
            {
                currentScale.y *= -1;
                gameObject.transform.localScale = currentScale;
            }
        }
         if (collision.gameObject.CompareTag("GravR"))
        {
            if (playerRigid.position.y > 0)
            {
                currentScale.y *= -1;
                gameObject.transform.localScale = currentScale;
            }
        }
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 currentScale = gameObject.transform.localScale;


        if (collision.gameObject.CompareTag("GravN"))
        {
            if (playerRigid.position.y > 0)
            {
                currentScale.y *= -1;
                gameObject.transform.localScale = currentScale;
            }
        }
        if (collision.gameObject.CompareTag("GravR"))
        {
            if (playerRigid.position.y < 0)
            {
                currentScale.y *= -1;
                gameObject.transform.localScale = currentScale;
                jumpSpeed = -jumpSpeed;
            }
        }

        /*
        //Damage by projectile
        if (collision.gameObject.CompareTag("Projectile"))
        {
            takeDmg.calcDmg(dmgAmount);
            Debug.Log("Test Damage!");
        }
        */
    }
}
