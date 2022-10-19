using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;

    // Movement variables
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public float jumpSpeed;
    [SerializeField]
    private int numFlips;

    // For input system
    private InputActionAsset playerControls;
    private InputActionMap player;
    private InputAction move;

    // Move direction variable
    Vector2 moveDir = Vector2.zero;
        
    // For ground checker.
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    // x Direction player faces
    bool facingRight = true;

    private void OnEnable()
    {
        player.FindAction("GravSwitch").started += GravSwitch;
        player.FindAction("Jump").started += Jump;
        move = player.FindAction("Move");
        player.Enable();
    }

    private void OnDisable()
    {
        player.Disable();
    }

    // On start defines input manager systems
    void Awake()
    {
        playerControls = this.GetComponent<PlayerInput>().actions;
        player = playerControls.FindActionMap("Player");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed,rb.velocity.y);
    }

    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isTouchingGround)
        {
            numFlips = 0;
        }

        //Flips direction player faces based on horizontal input
        moveDir = move.ReadValue<Vector2>();
        if (moveDir.x > 0 && !facingRight)
        {
            flipX();
        }
        if (moveDir.x < 0 && facingRight)
        {
            flipX();
        }
    }

    // Player reverses their gravity
    private void GravSwitch(InputAction.CallbackContext context)
    {
        if (numFlips < 2)
        {
            numFlips++;
            rb.gravityScale = -rb.gravityScale;
            jumpSpeed = -jumpSpeed;
            flipY();
        }
    }

    // Player jumps
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
        transform.Rotate(180f, 0f, 0f);
    }
}