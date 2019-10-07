using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public int playerLives;
    public int currentPlayerLives;
    public int characterLevel;
    public Animator characterAnimator;
    public myCharacterController myCharacter;

    public List<GameObject> reloadObjects;
    public List<GameObject> powerupObjects;
    public Switch switchObj;

    public void Start()
    {
       
    }

    public void InitializeData()
    {
        myCharacter.isKickingEnabled = false;
        //myCharacter.jumpLimit = 1;
        if (characterLevel == -1)
        {
            myCharacter.inSpiritForm = true;
        }
        else
        {
            myCharacter.inSpiritForm = false;
        }

    }

    public void ReloadScenceObjects()
    {
        //Debug.Log("rsrrs");
        switchObj.ResetSwitch();
        foreach (GameObject item in reloadObjects)
        {
            item.SetActive(true);
        }
    }

    public void ResetAllSceneObjects()
    {
        ReloadScenceObjects();
        foreach(GameObject item in powerupObjects)
        {
            item.SetActive(true);
        }
    }

    public void ResetCharacterInfo()
    {
        characterLevel = -1;
        currentPlayerLives = playerLives;

    }

    public void CollectPowerUp()
    {
        //Debug.Log("powerup!");
        characterLevel++;
        LevelUp();
    }

    public void DecreaseLife()
    {
        currentPlayerLives--;
        if(currentPlayerLives == 0)
        {
            MainManager.instance.GameOver();       
        }
    }

    public void LevelUp()
    {
        switch (characterLevel)
        {
            case 0:
                MainManager.instance.GameMusicStart();
                myCharacter.inSpiritForm = false;
                myCharacter.isKickingEnabled = false;
                myCharacter.jumpLimit = 1;
                
                break;
            case 1:
                myCharacter.isKickingEnabled = true;
                myCharacter.jumpLimit = 2;
                myCharacter.jumpRemaining = 2;
                break;
            case 2:
                myCharacter.isKickingEnabled = true;
                myCharacter.jumpLimit = 2;
                myCharacter.jumpRemaining = 2;
                break;
            case 3:
                myCharacter.isKickingEnabled = true;
                myCharacter.jumpLimit = 2;
                myCharacter.jumpRemaining = 2;
                break;
            case 4:
                myCharacter.isKickingEnabled = true;
                myCharacter.jumpLimit = 2;
                myCharacter.jumpRemaining = 2;
                break;

            case 5:
                myCharacter.isKickingEnabled = true;
                myCharacter.jumpLimit = 2;
                myCharacter.jumpRemaining = 2;
                break;

        }
    }
   


}
