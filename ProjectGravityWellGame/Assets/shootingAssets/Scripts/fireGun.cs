using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireGun : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //should add this to player movement instead then reference that here :)
        {
            Debug.Log("Shooting!");
            Shoot();
        }
    }

    // Spawns bullter at firePoint
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

}
