using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagRespawn : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private float respawnCD, respawnTime = 10;

    private bool countDown;

    void Start()
    {
        spawnPoint = gameObject.transform;
        respawnCD = respawnTime;
        countDown = false;
    }

    void Update()
    {
        if (countDown)
        {   
            respawnCD -= Time.deltaTime;
            if (respawnCD <= 0)
            {
                gameObject.transform.position = spawnPoint.position;
                gameObject.transform.rotation = spawnPoint.rotation;
            }
        }
        else
        {
            respawnCD = respawnTime;
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("FlagSpawn"))
        {
            countDown = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("FlagSpawn"))
        {
            countDown = false;
        }
    }
}