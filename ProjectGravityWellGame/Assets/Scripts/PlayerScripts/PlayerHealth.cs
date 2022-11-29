using UnityEngine;

//Make it so player goes invis on death
//Disable player collider
//Disable player rigidbody
//Enable all on respawn

public class PlayerHealth : MonoBehaviour
{
    public pickUpDropFire pUDF; // This script is so the player can drop their weapon when dead

    public int playerID; // Stores player ID for game status tracking
    public Vector3 startPos; //Spot where player will spawn at start

    //Temporary variables for score counter subject to change
    public int scoreCounter; //Current score player has
    public int deathCounter; //Number of deaths player has
    public int killCounter; //Number of kills player has

    // Player HP info
    public int maxHealth = 2;
    public int currentHealth;

    public bool isEliminated;

    //Respawn variables
    [SerializeField]
    private float respawnCD = 5; //How long it takes player to respawn
    private float respawnTimer; //How much longer until player respawns
    private bool playerDead;  //Tracks if player is currently daed

    private PlayerController pc; //Player controller script on player
    private SpriteRenderer sr;
    private Collider2D coll;

    // Death animation effect goes here when ready :)
    // public GameObject deathAni;

    void Start()
    {
        transform.position = startPos;  //When player joins spawns them at predetermined spawn point
    }

    //Also when player joins
    private void Awake()
    {
        currentHealth = maxHealth; //sets player health
        playerDead = false; //Sets player to alive
        killCounter = 0; //Sets player kills to 0
        deathCounter = 0; //Sets player deaths to 0
        scoreCounter = 0; //Sets player score to 0

        pc = GetComponent<PlayerController>(); //Gets the player controller from player
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<CapsuleCollider2D>();
        //respawnTimer = respawnCD;

        isEliminated = false;
    }

    private void Update()
    {
        if (isEliminated)
        {
            gameObject.SetActive(false);
        }

        // If player is dead, then start respawn timer
        if(GameManager.instance.gameMode == 1 || GameManager.instance.gameMode == 2)
        {
            if (playerDead)
            {
                respawnTimer -= Time.deltaTime;
                if (respawnTimer <= 0) //when respawn timer hits zero respawn the player
                {
                    playerRespawn(); //respawns player
                }
            }
        }
    }

    // Will calculate by subtracting currentHealth by damage amount
    public void calcDmg (int dmg, GameObject playerAttacker)
    {
        if (playerDead) return; //if player is already dead, then do nothing.

        currentHealth -= dmg; //Takes damage from bullet

        AudioManager.instance.playSFX(11);

        if (currentHealth <= 0) // If the players health hits zero or less, then player dies
        {
            if(GameManager.instance.gameMode == 0)
            {
                gameObject.SetActive(false);
                AudioManager.instance.playSFX(9);
            }
            if(GameManager.instance.gameMode == 1 || GameManager.instance.gameMode == 2)
            {
                playerDeath(playerAttacker); //kills player
            } 
        }
    }

    public void playerDeath()
    {
        // Plays player death animation when ready
        // Instantiate(deathAni, transform.position, Quaternion.identity);
        if (pUDF.weaponSlotFull) {pUDF.dropDead();}
        playerDead = true; //sets player to dead
        respawnTimer = respawnCD; //sets respawn timer
        coll.enabled = false;
        sr.enabled = false;
        pc.enabled = false; //turns off player ability to move while dead
        deathCounter++;
        AudioManager.instance.playSFX(9);
    }

    public void playerDeath(GameObject playerAttacker)
    {
        // Plays player death animation when ready
        // Instantiate(deathAni, transform.position, Quaternion.identity);
        if(pUDF.weaponSlotFull)
        {
            pUDF.dropDead();
        }
        if(playerAttacker.GetComponent<PlayerHealth>().playerID > 0)
        {
            playerAttacker.GetComponent<PlayerHealth>().killCounter++;
        }
        playerDead = true; //sets player to dead
        respawnTimer = respawnCD; //sets respawn timer
        coll.enabled = false;
        sr.enabled = false;
        pc.enabled = false; //turns off player ability to move while dead
        deathCounter++;
        AudioManager.instance.playSFX(9);
    }

    void playerRespawn()
    {
        playerDead = false; //player set to alive again
        respawnManager.instance.respawnAt(respawnManager.instance.randomRespawn(respawnManager.instance.pSP), gameObject);//respawns player at random place
        //add invul period
        coll.enabled = true;
        sr.enabled = true;
        pc.enabled = true; //gives player ability to move again
        currentHealth = maxHealth; //resets player health
        AudioManager.instance.playSFX(10);
    }

    public void FillHealth()
    {
        currentHealth = maxHealth;
    }
}