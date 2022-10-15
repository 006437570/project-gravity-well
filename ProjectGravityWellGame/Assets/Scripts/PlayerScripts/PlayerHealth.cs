using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public pickUpDropFire pUDF;

    public int maxHealth = 2;
    public int currentHealth;

    // Death animation effect goes here when ready :)
    // public GameObject deathAni;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    // Will calculate by subtracting currentHealth by damage amount
    public void calcDmg (int dmg)
    {
        currentHealth -= dmg;

        // If the players health hits zero or less, then player dies
        if (currentHealth <= 0)
        {
            pUDF.dropDead();
            playerDeath();
        }
    }

    void playerDeath()
    {
        // Plays player death animation when ready
        // Instantiate(deathAni, transform.position, Quaternion.identity);
        PlayerController pc = GetComponent<PlayerController>();
        pc.enabled = false;
        //add time stop
        mapManager.instance.playerRespawn(mapManager.instance.randomRespawn(mapManager.instance.pSP1, mapManager.instance.pSP2, mapManager.instance.pSP3, mapManager.instance.pSP4), gameObject);
        //add invul period
        pc.enabled = true;
        currentHealth = maxHealth;
    }
}