using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    private Animator anim;
    private bool isActivated;

    public PlatformMoveScript toMove;
    public PlatformMoveScript anotherToMove;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        isActivated = false;
    }

    public void ResetSwitch()
    {
        anim.Play("defaultSwitch");
        isActivated = false;
        anotherToMove.startMoving = false;
        isActivated = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isActivated && collision.collider.CompareTag("Player"))
        {
            if(MainManager.instance.currentPlayerInfo.characterLevel >=2)
            {
                anim.Play("switchAnimation");
                toMove.startMoving = true;
                anotherToMove.startMoving = true;
                isActivated = true;
            }
        
        }
    }


}
