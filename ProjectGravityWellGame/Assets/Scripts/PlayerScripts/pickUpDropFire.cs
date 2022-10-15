using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class pickUpDropFire : MonoBehaviour
{
    // For input system
    private InputActionAsset playerControls;
    private InputActionMap player;

    // Bools that determine weapon states
    [SerializeField]
    private bool inRange = false;
    [SerializeField]
    private bool weaponSlotFull = false;

    // Reference to gunHolder on player prefab
    [SerializeField]
    private Transform gunHolder;

    // References to weapon prefabs
    [SerializeField]
    private GameObject inHandItem;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Collider2D coll;

    [SerializeField]
    private fireProjectileGun gunScript;

    [SerializeField]
    private int numInRange = 0;

    private void OnEnable()
    {
        player.FindAction("Interact").started += Interact;
        player.FindAction("Fire").started += Fire;
        player.Enable();
    }

    private void OnDisable()
    {
        player.Disable();
    }

    void Awake()
    {
        playerControls = this.GetComponent<PlayerInput>().actions;
        player = playerControls.FindActionMap("Player");
    }

    // If player dies drops held weapon if has one
    // Called in PlayerHealth.cs
    public void dropDead()
    {
        if (weaponSlotFull)
        {
            weaponSlotFull = false;
            inHandItem.transform.SetParent(null);
            inHandItem = null;
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            coll.enabled = true;
            Debug.Log("Dying so dropping");
        }
        return;
    }

    // If player is in range of a weapon sets inRange to true
    // If outside of range sets to false
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Weapon"))
        {
            inRange = true;
            numInRange++;
            if (!weaponSlotFull)
            {
                inHandItem = collider.gameObject;
                rb = collider.GetComponent<Rigidbody2D>();
                coll = collider.GetComponent<CapsuleCollider2D>();
                gunScript = collider.GetComponent<fireProjectileGun>();
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Weapon"))
        {
            numInRange--;
            if (numInRange <= 0)
            {
                inRange = false;
                if (!weaponSlotFull)
                {
                    inHandItem = null;
                    rb = null;
                    coll = null;
                    gunScript = null;
                }
            }
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        // If player doesn't have a weapon equipped and is in range of weapon
        // then pick up weaon and place into gunHolder
        if (!weaponSlotFull && inRange)
        {
            weaponSlotFull = true;
            inHandItem.transform.position = Vector2.zero;
            inHandItem.transform.rotation = Quaternion.identity;
            inHandItem.transform.SetParent(gunHolder.transform, false);
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            coll.enabled = false;
            Debug.Log("Picking up");
            return;
        }
        // If player has a weapon equipped, then drop weapon
        if (weaponSlotFull)
        {
            weaponSlotFull = false;
            inHandItem.transform.SetParent(null);
            inHandItem = null;
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            coll.enabled = true;
            return;
        }
    }

    // Fires gun!
    private void Fire(InputAction.CallbackContext context)
    {
        if (weaponSlotFull)
        {
            gunScript.Shoot();
        }
    }
}