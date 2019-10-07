using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyManager : MonoBehaviour
{
    public bool startSpawn;
    public Transform[] spawnPoints;
    public GameObject flyingEnemyPrefab;
   
    public  Queue<GameObject> enemyQueue;

    public static FlyingEnemyManager instance;
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
    }

    private void Start()
    {
        enemyQueue = new Queue<GameObject>();
        startSpawn = false;
        GameObject temp;
        for(int i=0;i<10;i++)
        {
            temp = Instantiate(flyingEnemyPrefab) as GameObject;
            temp.SetActive(false);
            enemyQueue.Enqueue(temp);
        }
        StartCoroutine(SpawnEnemy());
    }
    
    IEnumerator SpawnEnemy()
    {
        int random = 0;
        int i = 0;
        while(i==0)
        {
            if(startSpawn)
            {
                random = Random.Range(0, 10);
                if(random >7)
                {
                    if (enemyQueue.Count > 0)
                    {
                        GameObject something = enemyQueue.Dequeue();
                        random = Random.Range(0, 4);
                        something.transform.position = spawnPoints[random].position;
                        something.SetActive(true);
                    }
                }
            }
            yield return new WaitForSeconds(2f);
        }
        yield return null;
    }

}
