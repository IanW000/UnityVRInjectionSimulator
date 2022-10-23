using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    public bool interactWithPotion;
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Potion")
        {
            interactWithPotion = true;
        }
        Debug.Log(collision.gameObject.name);
    }
    private void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.tag == "Potion")
        {
            interactWithPotion = false;
        }
        Debug.Log(interactWithPotion + "lpl");
    }
}
