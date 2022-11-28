using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public void HoverSound() 
    {
        AudioManager.instance.playSFX(5);
    }
    
    public void OnClick() 
    {
        AudioManager.instance.playSFX(6);
    }
}
