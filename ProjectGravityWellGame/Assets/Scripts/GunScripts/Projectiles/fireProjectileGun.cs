using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireProjectileGun : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //should add this to player movement instead then reference that here :)
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
