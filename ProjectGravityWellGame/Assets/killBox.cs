using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killBox : MonoBehaviour
{
    // kills the player if they collide with anything that has this script attached
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if(GameManager.instance.gameMode == 0)
            {
                collider.gameObject.SetActive(false);
            }
            if(GameManager.instance.gameMode == 1 || GameManager.instance.gameMode == 2)
            {
                collider.GetComponent<PlayerHealth>().playerDeath();
            }
        }
        else if (collider.gameObject.CompareTag("GunHolder"))
        {
        }
        else if (collider.gameObject.CompareTag("Weapon"))
        {
            if(collider.gameObject.GetComponent<fireProjectileGun>().isFlag)
            {
                return;
            }
        }
        else
        {
            Destroy(collider.gameObject);
        }
    }
}
