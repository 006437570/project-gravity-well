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

    public float jumpSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    // Start is called before the first frame update
    void Awake()
    {

        playerRigid = GetComponent<Rigidbody2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        input = new Vector2(Input.GetAxis("Horizontal") * speed, playerRigid.velocity.y);

        playerRigid.AddForce(input * speed * Time.deltaTime);

        moving = (input.x != 0 || input.y != 0);

        //Alternative Force: Attatches inputs to player object directly
        //playerRigid.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, playerRigid.velocity.y);
        //moving = (playerRigid.velocity.x != 0 || playerRigid.velocity.y != 0);

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            playerRigid.velocity = new Vector2(playerRigid.velocity.x, jumpSpeed);
        }

    }
}
