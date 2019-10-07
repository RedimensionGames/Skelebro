using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public animationType thisType;
    private Animator animator;
    public enum animationType
    {
        Head,
        Body,
        Arm,
        Leg
    }

    private void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        switch (thisType)
        {
            case animationType.Head:
                animator.Play("head");
                break;
            case animationType.Body:
                animator.Play("body");
                break;
            case animationType.Arm:
                animator.Play("arm");
                break;
            case animationType.Leg:
                animator.Play("leg");
                break;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("collided");
            ConsumePowerUp();          
        }
    }

    private void ConsumePowerUp()
    {
        SoundManager.instance.PowerUpSFX();
        this.gameObject.SetActive(false);
        MainManager.instance.currentPlayerInfo.CollectPowerUp();      
    }

    

}
