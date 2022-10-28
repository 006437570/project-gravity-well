using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.GetComponent<PlayerHealth>().playerDeath();
            
        }
        else if (collider.gameObject.CompareTag("GunHolder"))
        {
        }
        else
        {
            Destroy(collider.gameObject);
        }
    }
}
