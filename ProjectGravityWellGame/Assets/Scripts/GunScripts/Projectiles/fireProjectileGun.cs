using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireProjectileGun : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    void Update()
    {
        //move to player controller
        //melee when not holding gun
        if (Input.GetButtonDown("Fire1")) //should add this to player movement instead then reference that here :)
        {
            Shoot();
        }
    }

    // Spawns bullter at firePoint
    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

}

//make this a script for every projectile weapon