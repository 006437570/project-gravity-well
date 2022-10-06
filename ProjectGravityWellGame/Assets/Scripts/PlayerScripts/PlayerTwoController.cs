using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoController : MonoBehaviour
{
    Rigidbody2D playerRigid;
    Vector2 input;
    public float dirX, dirY, movingspeed;
    public float speed;
    bool moving = false;

    float inputHorizontal;

    public float jumpSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    bool facingRight = true;

    private Vector2 var; 

    // Start is called before the first frame update
    void Awake()
    {

        playerRigid = GetComponent<Rigidbody2D>();
        movingspeed = 5f;

    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        input = new Vector2(Input.GetAxis("HorizontalTwo") * speed, playerRigid.velocity.y);

          //playerRigid.AddForce(input * speed * Time.deltaTime);

           inputHorizontal = Input.GetAxisRaw("HorizontalTwo");


        var = input * speed * Time.deltaTime;

        movingspeed = var.x;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
            playerRigid.velocity = new Vector2(-speed, playerRigid.velocity.y);
              
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
            playerRigid.velocity = new Vector2(speed, playerRigid.velocity.y);

        }

       
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

        if (Input.GetKey(KeyCode.RightShift) && isTouchingGround)
        {
            Debug.Log("Player 2 Jump!");
            playerRigid.velocity = new Vector2(playerRigid.velocity.x, jumpSpeed);
        }
        
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
    }
}
