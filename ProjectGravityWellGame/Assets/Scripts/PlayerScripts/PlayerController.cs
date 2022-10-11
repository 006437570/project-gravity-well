using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction playerControler;
    

    Rigidbody2D playerRigid;
    Vector2 input;
    Vector2 moveDir = Vector2.zero;
    public float movingspeed;
    public float speed;
    bool moving = false;

    float inputHorizontal;

    public float jumpSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    bool facingRight = true;

    // Start is called before the first frame update
    void Awake()
    {

        playerRigid = GetComponent<Rigidbody2D>();
        movingspeed = 5f;
        
    }
    private void OnEnable()
    {
        playerControler.Enable();
       
    }
    private void OnDisable()
    {
        playerControler.Disable();
        
    }
    private void FixedUpdate()
    {
        playerRigid.velocity = new Vector2(moveDir.x * speed ,playerRigid.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //input = new Vector2(Input.GetAxis("Horizontal") * speed, playerRigid.velocity.y);

      //  playerRigid.AddForce(input * Time.deltaTime);

      //  inputHorizontal = Input.GetAxisRaw("Horizontal");

        moveDir = playerControler.ReadValue<Vector2>();

        
        
            /*
        if (name == "PlayerOne")
        {
           
            if (Input.GetKey(KeyCode.A)){
                
               playerRigid.velocity = new Vector2(-speed, playerRigid.velocity.y);
               // playerRigid.AddForce(input * Time.deltaTime);
            }

            if  (Input.GetKey(KeyCode.D)){
                playerRigid.velocity = new Vector2(speed, playerRigid.velocity.y);
              //  playerRigid.AddForce(input * Time.deltaTime);
            }
        }
        */
       
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

        if (name == "PlayerOne")
        {

            if (Input.GetButtonDown("Jump") && isTouchingGround)
            {
                playerRigid.velocity = new Vector2(playerRigid.velocity.x, jumpSpeed);
            }

        }

        if (name == "PlayerTwo")
        {

            if (Input.GetButtonDown("JumpTwo") && isTouchingGround)
            {
                playerRigid.velocity = new Vector2(playerRigid.velocity.x, jumpSpeed);
            }

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
