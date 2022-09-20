using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireHitscanGun : MonoBehaviour
{

    public Transform firePoint;

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
        RaycastHit2D hitReg = Physics2D.Raycast(firePoint.position, firePoint.right);

        if(hitReg)
        {
            Debug.Log(hitReg.transform.name);
        }
    }

}
