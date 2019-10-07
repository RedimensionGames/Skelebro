using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private bool isActivated;
    private Animator anima;
    
    void Start()
    {
        anima = gameObject.GetComponent<Animator>();
        isActivated = false;
    }

   
    void Update()
    {
        if(isActivated)
        {
            anima.Play("candle");

        }
        else
        {
            anima.Play("default");
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {  
        if (collision.CompareTag("Player"))
        {
            ActivateThisCheckpoint();
            //Debug.Log("SDASD");
        }
    }
       

    private void ActivateThisCheckpoint()
    {
        if (MainManager.instance.checkpointSpawnPoint != null)
        {
            MainManager.instance.checkpointSpawnPoint.GetComponent<Checkpoint>().DeactivateThisCheckpoint();
        }
        isActivated = true;
        MainManager.instance.checkpointSpawnPoint = this.gameObject;
    }

    public void DeactivateThisCheckpoint()
    {
        isActivated = false;
    }
    
}
