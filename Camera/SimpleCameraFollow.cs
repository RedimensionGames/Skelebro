using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public Transform objectToFollow;
    private Vector3 followCoordinates;
    
    void Update()
    {
        if (MainManager.instance.currentGameState == MainManager.GameState.Game)
        {
            followCoordinates.x = objectToFollow.position.x;
            followCoordinates.y = 4.5f;
            followCoordinates.z = -10;
            transform.position = followCoordinates;
        }
    }
}
