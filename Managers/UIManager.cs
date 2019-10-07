using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public GameObject mainMenuPanel;
    public GameObject pausedPanel;
    public GameObject gameOverPanel;
    public GameObject endPanel;
    public GameObject HUD;

    public GameObject[] livesObjects;

    public static UIManager instance;
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
    
    public void HideAllPanels()
    {
        mainMenuPanel.SetActive(false);
        pausedPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        endPanel.SetActive(false);
        HUD.SetActive(false);
    }

    public void ShowMainMenu()
    {
        HideAllPanels();
        mainMenuPanel.SetActive(true);
    }

    public void ShowPausePanel()
    {
        HideAllPanels();
        pausedPanel.SetActive(true);
    }

    public void OnClickPlay()
    {
        HideAllPanels();
        HUD.SetActive(true);
        MainManager.instance.StartGame();
    }
    public void OnClickResume()
    {
        UIManager.instance.HideAllPanels();
        HUD.SetActive(true);
        MainManager.instance.ResumeGame();
    }

    public void OnClickQuit()
    {
        HideAllPanels();
        SoundManager.instance.MainMenuPlaySound();
        MainManager.instance.QuitGame();
    }

    public void OnClickRestart()
    {
        HideAllPanels();
        HUD.SetActive(true);
        MainManager.instance.StartGame();
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void UpdateLivesInUI(int amount)
    {
        int i = 0;
        int j=livesObjects.Length;
        for(;i<amount;i++)
        {
            livesObjects[i].SetActive(true);
        }
        for(;i<j;i++)
        {
            livesObjects[i].SetActive(false);
        }
    }


    public void ShowEndPanel()
    {
        endPanel.SetActive(true);
    }

    public void ResetUI()
    {
        
            
        foreach (GameObject item in livesObjects)
        {
            item.SetActive(true);
        }
    }

}
