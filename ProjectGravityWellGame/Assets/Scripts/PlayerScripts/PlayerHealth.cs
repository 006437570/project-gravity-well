using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth = 2;

    // Death animation effect goes here when ready :)
    // public GameObject deathAni;

    // Will calculate by subtracting currentHealth by damage amount
    public void calcDmg (int dmg)
    {
        currentHealth -= dmg;

        // If the players health hits zero or less, then player dies
        if (currentHealth <= 0)
        {
            playerDeath();
        }
    }

    void playerDeath()
    {
        // Plays player death animation when ready
        // Instantiate(deathAni, transform.position, Quaternion.identity);

        // When player dies it disables their movement until respawn
        PlayerController pc = GetComponent<PlayerController>();
        pc.enabled = false;
    }
}