using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelect : MonoBehaviour
{

    [SerializeField] int color1, color2, color3, color4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerController thePlayer = other.GetComponent<PlayerController>();
            thePlayer.gameObject.GetComponent<SpriteRenderer>().color = new Color(color1, color2, color3, color4);
        }
    }
}
