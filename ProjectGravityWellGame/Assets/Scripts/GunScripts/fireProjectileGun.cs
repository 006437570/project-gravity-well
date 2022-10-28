using UnityEngine;

public class fireProjectileGun : MonoBehaviour
{
    // Rework this script

    public Transform firepoint; //point from gun that bullet is shot from
    public GameObject bulletPrefab; //bullet prefab that will be shot

    public int magazineSize; // sets the magazinesize per each weapon is modifiable when adding new weapons
    public int ammo; // is what gets checked in the function below


    // During the start of the game set the ammo to whatever the set magazine size is set to
    void Start()
    {
        ammo = magazineSize;
    }

    public void Shoot(GameObject playerAttacker)
    {   
        if (ammo > 0) // Check if ammo is greater than 0 if so then allow the player to shoot
        {
            bulletPrefab.GetComponent<bulletProjectile>().playerAttacker = playerAttacker;
            Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            ammo--; // reduce ammo by 1 every time a shot is fired
        }
        else
        {
            //audio
        }
    }
}