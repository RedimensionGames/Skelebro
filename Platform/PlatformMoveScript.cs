using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveScript : MonoBehaviour
{
    public bool startMoving;
    public float moveSpeed;
    public float offsetX;
    public float offsetY;
    //public float offsetY;
    private Vector3 currentTarget;
    private Vector3 rightPos;
    private Vector3 leftPos;
    private Vector3 upPos;
    private Vector3 downPos;

    private void Start()
    {
        rightPos = transform.position;
        rightPos.x += offsetX;
        leftPos = transform.position;
        upPos = transform.position;
        upPos.y += offsetY;
        downPos = transform.position;

    }

    void Update()
    {
        if (MainManager.instance.currentGameState == MainManager.GameState.Game && startMoving)
        {
            SimpleMove();
        }
    }

    void SimpleMove()
    {
        if (offsetY == 0)
        {
            if (transform.localPosition == rightPos)
            {
                currentTarget = leftPos;
            }
            else if (transform.localPosition == leftPos)
            {
                currentTarget = rightPos;
            }
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, currentTarget, Time.deltaTime * moveSpeed);
        }
        if (offsetX == 0)
        {
            if (transform.localPosition == downPos)
            {
                currentTarget = upPos;
            }
            else if (transform.localPosition == upPos)
            {
                currentTarget = downPos;
            }
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, currentTarget, Time.deltaTime * moveSpeed);
        }
    }

}
