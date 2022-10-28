using UnityEngine;

public class weaponPad : MonoBehaviour
{
    // initializing many variables to store data on weapons and spawnpoints
    [SerializeField]
    private GameObject weapon;

    [SerializeField]
    private Transform weaponSP;

    [SerializeField]
    private float spawnCD, timerCD;

    [SerializeField]
    private bool spawnAtStart, onSpawnPoint;

    // On awake start the spawning weapons
    private void Awake()
    {
        if (spawnAtStart)
        {
            onSpawnPoint = true;
            spawnWeapon();
        }
        if (!spawnAtStart)
        {
            timerCD = spawnCD;
            onSpawnPoint = false;
        }
    }

    // When an object leaves the trigger collider do a check on the spawnpoint
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Weapon"))
        {
            timerCD = spawnCD;
            onSpawnPoint = false;
        }
    }

    // Checks if an object enters the trigger collider attached to this object do a check on the spawnpoint
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Weapon"))
        {
            onSpawnPoint = true;
        }
    }

    // After every in game frame update check if there is a weapon on a spawnpoint if not then
    // attempt to spawn a weapon based on the time
    private void Update()
    {
        if (!onSpawnPoint)
        {
            timerCD -= Time.deltaTime;
            if (timerCD <= 0)
            {
                onSpawnPoint = true;
                spawnWeapon();
            }
        }
    }

    // spawns a weapon when called
    void spawnWeapon()
    {
        Instantiate(weapon, weaponSP.position, Quaternion.identity);
    }
}