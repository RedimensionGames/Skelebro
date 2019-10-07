using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    public float moveSpeed;
    public Transform wayPoint1;
    public Transform wayPoint2;
    public Vector3 currentTarget;

    private void Start()
    {
        currentTarget = wayPoint1.localPosition;
    }
    void Update()
    {
        if(MainManager.instance.currentGameState== MainManager.GameState.Game)
        {
            SimpleMove();
        }       
    }

    void SimpleMove()
    {
        if (transform.localPosition == wayPoint1.localPosition)
        {
            currentTarget = wayPoint2.localPosition;
        }
        else if(transform.localPosition == wayPoint2.localPosition)
        {
            currentTarget = wayPoint1.localPosition;
        }
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, currentTarget, Time.deltaTime * moveSpeed);
    }
}
