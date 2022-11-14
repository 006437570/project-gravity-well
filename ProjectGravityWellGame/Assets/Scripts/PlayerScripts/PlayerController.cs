using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Interacts with the rigidbody
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
    public LayerMask gunlayer;
    public LayerMask playerlayer;
    private bool isTouchingGround;
    private bool isTouchingGun;
    private bool isTouchingPlayer;
    private bool isJumping;

    // x Direction player faces
    bool facingRight = true;


    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        //GameManager.instance.AddPlayer(this);
    }

    // When an object in this case the player becomes enabled give the player movement
    private void OnEnable()
    {
        player.FindAction("GravSwitch").started += GravSwitch;
        player.FindAction("Jump").started += Jump;
        move = player.FindAction("Move");
        player.Enable();
    }

    // When an object is disabled in this case disable the player
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

    // messes with the physics of the rigidbody
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, rb.velocity.y);
    }

    // Whenever the game updates every frame do some preliminary checks
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); // checks if the player is touching the ground

        isTouchingGun = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, gunlayer); // checks if the player is touching a gun

        isTouchingPlayer = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, playerlayer); // checks if the player is touching another player

        if (isTouchingGround) // check if the player is touching the ground
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
            isJumping = true;
        }
    }

    // Player jumps
    private void Jump(InputAction.CallbackContext context)
    {
        if (isTouchingGround || isTouchingGun || isTouchingPlayer && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            isJumping = true;
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
        //AudioManager.instance.playSFX(2);
        transform.Rotate(180f, 0f, 0f);
    }

    // Checks if the player is colliding with objects on the map and if so then set isJumping to false
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Terrain") || other.gameObject.CompareTag("Weapon") || other.gameObject.CompareTag("Player"))
        {
            isJumping = false;
        }
    }
}