using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpSpeed;

    public PlayerInputActions playerControls;

    Vector2 moveDir = Vector2.zero;

    private InputAction move;
    private InputAction jump;
    
    Vector2 input;
    
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    bool facingRight = true;

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += Jump;
    }

    private void OnDisable()
    {
        move.Disable();        
        jump.Disable();
    }

    void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
        playerControls = new PlayerInputActions();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed,rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        moveDir = move.ReadValue<Vector2>();
       
        if (moveDir.x > 0 && !facingRight)
        {
            flipX();
        }
        if (moveDir.x < 0 && facingRight)
        {
            flipX();
        }
        
        //Alternative Force: Attatches inputs to player object directly
        //playerRigid.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, playerRigid.velocity.y);
        //moving = (playerRigid.velocity.x != 0 || playerRigid.velocity.y != 0);
        
        // Flips players gravity and flips them upside down
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rb.gravityScale = -rb.gravityScale;
            jumpSpeed = -jumpSpeed;
            flipY();
        }

    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (isTouchingGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    // Flips player horizontally
    void flipX()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }

    // Flips player vertically
    void flipY()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.y *= -1;
        gameObject.transform.localScale =  currentScale;
    }
}