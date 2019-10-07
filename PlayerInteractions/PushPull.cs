using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPull : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PushObject"))
        {
            myCharacterController.instance.isReadyToPush = true;
            myCharacterController.instance.objectToPush = collision.gameObject;
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PushObject"))
        {
            myCharacterController.instance.isReadyToPush = false;
            //myCharacterController.instance.objectToPush = null;
        }
    }
}
