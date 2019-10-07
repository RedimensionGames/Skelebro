using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float enemySpeed = 5f;
    public float enemyTimer = 10;
    private float currentTimer;
    void Update()
    {
        if (MainManager.instance.currentGameState == MainManager.GameState.Game)
        {
            transform.Translate(Vector3.left * enemySpeed * Time.deltaTime);
            if (currentTimer > enemyTimer)
            {
                gameObject.SetActive(false);
                currentTimer = 0;
                FlyingEnemyManager.instance.enemyQueue.Enqueue(gameObject);
            }
            else
            {
                currentTimer += Time.deltaTime;
            }
        }
       
    }
}
