using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrollMainController : MonoBehaviour
{
    public BGScroll[] scrollReference;
    public float scrollSpeed1;
    public float scrollSpeed2;
    public float scrollSpeed3;
    public float scrollSpeed4;

    public void Walk()
    {
        scrollReference[0].scrollSpeed = scrollSpeed1;
        scrollReference[1].scrollSpeed = scrollSpeed2;
        scrollReference[2].scrollSpeed = scrollSpeed3;
        scrollReference[3].scrollSpeed = scrollSpeed4;
    }

    public void ReverseWalk()
    {
        scrollReference[0].scrollSpeed = -scrollSpeed1;
        scrollReference[1].scrollSpeed = -scrollSpeed2;
        scrollReference[2].scrollSpeed = -scrollSpeed3;
        scrollReference[3].scrollSpeed = -scrollSpeed4;
    }

    public void Stop()
    {
        scrollReference[0].scrollSpeed = 0;
        scrollReference[1].scrollSpeed = 0;
        scrollReference[2].scrollSpeed = 0;
        scrollReference[3].scrollSpeed = 0;
    }

    /*
    public float scrollSpeed;


    private BoxCollider2D currentCollider;
    private float currentColliderLength;




    private void Start()
    {
        //currentCollider = GetComponent<BoxCollider2D>();
        currentColliderLength = currentCollider.size.x;
    }

    private void Update()
    {
        if (transform.position.x < -currentColliderLength)
        {
            RepositionBG();
        }
    }

    private void RepositionBG()
    {
        Vector2 offset = new Vector2(currentColliderLength * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }*/


}
