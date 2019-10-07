using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration;
    public float shakeMagnitude;
    private bool shakingDone;

    private float xShake;
    private float yShake;
    private float timeElapsed;
    private Vector3 currentPos;

    private void Start()
    {
        xShake = 0;
        yShake = 0;
        timeElapsed = 0;
        currentPos = this.transform.position;
        shakingDone = false;
    }

    public void StartShake()
    {
        shakingDone = false;
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        
        currentPos = this.transform.position;
        while ((timeElapsed< shakeDuration)&& !shakingDone)
        {
            timeElapsed += Time.deltaTime+0.05f;
            xShake = Random.Range(-1f, 1f) * shakeMagnitude;
            yShake = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.localPosition = new Vector3(currentPos.x+xShake, currentPos.y+yShake, currentPos.z);
            yield return null;
        }
        timeElapsed = 0;
        shakingDone = true;
        while (timeElapsed < 0.7f)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        timeElapsed = 0;      
        transform.localPosition = currentPos;
        //Debug.Log("SADASDA");
        MainManager.instance.ResetToCheckpoint();
        yield return null;
    }
}
