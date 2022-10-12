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

    private InputActionAsset playerControls;
    private InputActionMap player;
    private InputAction move;

    Vector2 moveDir = Vector2.zero;
        
    // For ground checker.
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    bool facingRight = true;

    private void OnEnable()
    {
        player.FindAction("Fire").started += Fire;
        player.FindAction("GravSwitch").started += GravSwitch;
        player.FindAction("Jump").started += Jump;
        move = player.FindAction("Move");
        player.Enable();
    }

    private void OnDisable()
    {
        player.Disable();
    }

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

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Shooting");
    }

    // Player reverses their gravity
    private void GravSwitch(InputAction.CallbackContext context)
    {
        rb.gravityScale = -rb.gravityScale;
        jumpSpeed = -jumpSpeed;
        flipY();
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
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.y *= -1;
        gameObject.transform.localScale =  currentScale;
    }
}