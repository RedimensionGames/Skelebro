using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShow : MonoBehaviour
{

    public Animator anim;

    public static ButtonShow instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Duplicate Found!");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        if(!anim.gameObject.activeSelf)
        {
            anim.gameObject.SetActive(true);
        }
    }



    public void PlayAnimation(int index)
    {
        switch(index)
        {
            case 0:
                anim.Play("buttonDirections");
                break;
            case 1:
                anim.Play("buttonDoubleSpace");
                
                break;
            case 2:
                anim.Play("buttonB");
                break;
            case 3:
                anim.Play("buttonM");
                break;
            case 4:
                anim.Play("buttonLeft");
                break;
            case 5:
                anim.Play("buttonRight");
                break;
            case 6:
                anim.Play("buttonSpace");
                break;
            case 7:
                anim.Play("buttonD");
               
                break;
            default:
                anim.Play("buttonNone");
                break;
        }
    }

}
