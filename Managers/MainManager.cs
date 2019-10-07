using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public GameObject playerCharacter;
    public Vector3 playerStartingPosition;
    public GameObject checkpointSpawnPoint;
    public GameObject initialStartPoint;
 
    public CharacterInfo currentPlayerInfo;
    public myCharacterController charController;
    public BGScrollMainController scrollController;

    public GameState currentGameState;
    public enum GameState
    {
        MainMenu,
        Game,
        GamePaused,
        InputDisabled,
        Ending
    }

    public static MainManager instance;
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

    public void ResetToCheckpoint( )
    {

        playerCharacter.transform.position = checkpointSpawnPoint.transform.position;
        charController.resettingFlag = false;
        currentPlayerInfo.ReloadScenceObjects();
        currentGameState = GameState.Game;
    }

    void Start()
    {
        checkpointSpawnPoint = initialStartPoint;
        playerStartingPosition = checkpointSpawnPoint.transform.position;
        currentGameState = GameState.MainMenu;
        UIManager.instance.ShowMainMenu();
        currentPlayerInfo.InitializeData();
        myCharacterController.instance.InitializeCC();
        MenuMusicStart();
       
    }

    public void StartGame()
    {
        ResetGame();
        currentGameState = GameState.Game;
    }

    public void PauseGame()
    {
        currentGameState = GameState.GamePaused;
        UIManager.instance.ShowPausePanel();
    }

    public void ResumeGame()
    {
        currentGameState = GameState.Game;
        
    }

    public void QuitGame()
    {
        currentGameState = GameState.MainMenu;
        UIManager.instance.ShowMainMenu();
        ResetGame();
    }

    private void ResetGame()
    {
   
        playerCharacter.transform.position = playerStartingPosition;
        currentPlayerInfo.ResetCharacterInfo();
        currentPlayerInfo.ResetAllSceneObjects();
        someButton.SetActive(false);
        UIManager.instance.ResetUI();
    }

    public void PlayerGetHit()
    {
        currentPlayerInfo.DecreaseLife();
        UIManager.instance.UpdateLivesInUI(currentPlayerInfo.currentPlayerLives);
    }

    public void GameOver()
    {
        currentGameState = GameState.GamePaused;
        UIManager.instance.ShowGameOverPanel();
    }

    public void StartMovingBG(bool mode)
    {
        if (mode)
            scrollController.Walk();
        else
            scrollController.ReverseWalk();
    }

    public void StopMovingBG()
    {
        scrollController.Stop();
    }


    public void MenuMusicStart()      
    {
        SoundManager.instance.MainMenuPlaySound();
    }

    public void GameMusicStart()
    {
        SoundManager.instance.GameStartMusic();
    }

    public void PlayEnding()
    {
        SoundManager.instance.PlayEndingSong();
        currentGameState = GameState.Ending;
        StartCoroutine(endRoutine());
    }

    public GameObject someButton;
  
    IEnumerator endRoutine()
    {
        yield return new WaitForSeconds(5f);
        UIManager.instance.ShowEndPanel();
        someButton.SetActive(true);
        yield return null;
    }

}
