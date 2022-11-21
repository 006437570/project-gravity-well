using UnityEngine;
using System.Collections;

public class fireProjectileGun : MonoBehaviour
{
    public bool isFlag; // bool that determines if the weapon is actually a flag or not

    // Rework this script
    public Transform firepoint; //point from gun that bullet is shot from
    public GameObject bulletPrefab; //bullet prefab that will be shot

    public int magazineSize; // sets the magazinesize per each weapon is modifiable when adding new weapons
    public int ammo; // is what gets checked in the function below
    
    public int ammoPerShot;

    [SerializeField]
    private float burstDelay, shotDelay; // Cooldown before player can shoot again and burst

    private bool isShooting; // Checks if the player is currently firing their weapon


    // During the start of the game set the ammo to whatever the set magazine size is set to
    void Start()
    {
        ammo = magazineSize;
        isShooting = false;
    }

    public IEnumerator Shoot(GameObject playerAttacker)
    {
        if (ammo > 0 && !isShooting) // Check if ammo is greater than 0 and if player isnt shooting if so then allow the player to shoot
        {
            isShooting = true;
            // fire gun sfx
            for (int i = 0; i < ammoPerShot; i++)
            {
                bulletPrefab.GetComponent<bulletProjectile>().playerAttacker = playerAttacker;
                Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
                ammo --; // reduce ammo by 1 every time a shot is fired
                yield return new WaitForSeconds(burstDelay);
            }
            yield return new WaitForSeconds(shotDelay);
            isShooting = false;
        }
        else
        {
            // ammo empty sfx
        }
    }
}

/*
 public IEnumerator FireBurst(GameObject bulletPrefab, int burstSize, float rateOfFire)
 {
     float bulletDelay = 60 / rateOfFire; 
     // rate of fire in weapons is in rounds per minute (RPM), therefore we should calculate how much time passes before firing a new round in the same burst.
     for (int i = 0; i < burstSize; i++)
     {
         GameObject bullet = Instatiate(bulletPrefab); // It would be wise to use the gun barrel's position and rotation to align the bullet to.
         bullet.GetComponent<Rigidbody>.AddForce(bullet.transform.forward * 4000); // add some force to your bullet's rigidbody to make it go forward
         
         yield return new WaitForSeconds(bulletDelay); // wait till the next round
     }
 }
*/