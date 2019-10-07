using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource source;
    public AudioSource SFXsource;

    public AudioClip mainMenuClip;
    public AudioClip game1;
    public AudioClip game2;
    public AudioClip endClip;

    public AudioClip[] SFXClips;

    private float timer;
    private float clipLength;

    public static SoundManager instance;
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


    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

   

    public void MainMenuPlaySound()
    {
        source.Stop();
        source.clip = mainMenuClip;
        source.loop = true;
        source.Play();
    }

    public void GameStartMusic()
    {
        source.Stop();
        source.clip = game1;
        source.loop = false;
        clipLength = game1.length;
        source.Play();
        StartCoroutine(counter());
    }

    public void PlayEndingSong()
    {
        source.Stop();
        source.clip = endClip;
        source.loop = false;
        source.Play();
    }

    IEnumerator counter()
    {
        while (timer < clipLength)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0;
        source.Stop();
        source.loop = true;
        source.clip = game2;
        source.Play();
        yield return null;
    }

    public void KickSFX()
    {
        SFXsource.clip = SFXClips[0];
        SFXsource.Play();
    }

    public void PowerUpSFX()
    {
        SFXsource.clip = SFXClips[1];
        SFXsource.Play();
    }

    public void JumpSFX()
    {
        SFXsource.clip = SFXClips[2];
        SFXsource.Play();
    }

    public void FallSFX()
    {
        SFXsource.clip = SFXClips[3];
        SFXsource.Play();
    }

    public void HurtSFX()
    {
        SFXsource.clip = SFXClips[4];
        SFXsource.Play();
    }

}
