using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    [SerializeField] MultiplayerInputManager inputManager;
    [SerializeField] GameManager gameManager;

    string previousScene;
    public bool storyMode;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        inputManager = MultiplayerInputManager.instance;
        gameManager = GameManager.instance;

        storyMode = false;
    }

    private void Update()
    {
        if (inputManager == null)
        {
            inputManager = MultiplayerInputManager.instance;
        }

        if (gameManager == null)
        {
            gameManager = GameManager.instance;
        }
    }

    public void SetPreviousScene()
    {
        previousScene = SceneManager.GetActiveScene().name;
        Debug.Log("Previous scene: " +  previousScene);
    }

    public void LoadStoryMode()
    {
        storyMode = true;
        SceneManager.LoadScene("IntroAnimatic");
    }

    public void LoadMainMenu()
    {
        SetPreviousScene();
        gameManager.ResetInstance();
        inputManager.ResetInstance();
        SceneManager.LoadScene("MainMenu");

        // Play main theme when loading the main menu
        AudioManager.instance.Play("MainTheme");
    }

    public void LoadCharacterSelection()
    {
        SetPreviousScene();
        SceneManager.LoadScene("CharacterSelection");
    }


    public void LoadMinigameSelection()
    {
        SetPreviousScene();
        
        if (SceneManager.GetActiveScene().name == "CharacterSelection")
        {
            if (gameManager.players.Count >= 2)
            {
                SceneManager.LoadScene("MinigameSelection");
            }
            else
            {
                Debug.Log("Not enough players");
            }
        }
        else
        {
            SceneManager.LoadScene("MinigameSelection");
        }

    }


    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(previousScene);
        previousScene = "Scores";
    }

    //  Load Minigames
    #region load minigames
    public void LoadMazeMinigame()
    {
        SetPreviousScene();
        SceneManager.LoadScene("MazeMinigame");
    }

    public void LoadRaceMinigame()
    {
        SetPreviousScene();
        SceneManager.LoadScene("RaceMinigame");
    }

    public void LoadDodgeballMinigame()
    {
        SetPreviousScene();
        SceneManager.LoadScene("DodgeballMinigame");
    }

    public void LoadBumperMinigame()
    {
        SetPreviousScene();
        SceneManager.LoadScene("BumperMinigame");
    }

    public void LoadTugOWarMinigame()
    {
        SetPreviousScene();
        SceneManager.LoadScene("TugOWarMinigame 1");
    }
    #endregion



    //  Load Instructions
    #region load instructions
    public void LoadMazeInstructions()
    {
        SetPreviousScene();
        SceneManager.LoadScene("MazeInstructionScreen");
        // Play instruction music when loading instructions
        //AudioManager.instance.Play("InstructionMusic");
    }

    public void LoadRaceInstructions()
    {
        SetPreviousScene();
        SceneManager.LoadScene("RaceInstructionScreen");
        // Play instruction music when loading instructions
        //AudioManager.instance.Play("InstructionMusic");
    }

    public void LoadDodgeballInstructions()
    {
        SetPreviousScene();
        SceneManager.LoadScene("DodgeballInstructionScreen");
        // Play instruction music when loading instructions
        //AudioManager.instance.Play("InstructionMusic");
    }

    public void LoadBumperInstructions()
    {
        SetPreviousScene();
        SceneManager.LoadScene("BumperInstructionScreen");
        // Play instruction music when loading instructions
        //AudioManager.instance.Play("InstructionMusic");
    }

    public void LoadTugOWarInstructions()
    {
        SetPreviousScene();
        SceneManager.LoadScene("TugOWarInstructionScreen");
        // Play instruction music when loading instructions
        //AudioManager.instance.Play("InstructionMusic");
    }
    #endregion

    public void LoadTotalScores()
    {
        SceneManager.LoadScene("TotalScores");
    }

    //switch cases
    public void LoadMinigame()
    {
        SetPreviousScene();
        string currentSceneName = SceneManager.GetActiveScene().name;

        switch (currentSceneName)
        {
            case "BumperInstructionScreen":
                LoadBumperMinigame();
                break;

            case "DodgeballInstructionScreen":
                LoadDodgeballMinigame();
                break;

            case "MazeInstructionScreen":
                LoadMazeMinigame();
                break;

            case "RaceInstructionScreen":
                LoadRaceMinigame();
                break;

            case "TugOWarInstructionScreen":
                LoadTugOWarMinigame();
                break;
        }
    }

    

    public void LoadInstructions()
    {
        //SetPreviousScene();
        

        switch (previousScene)
        {
            case "MazeMinigame":
                LoadDodgeballInstructions();
                break;

            case "DodgeballMinigame":
                LoadRaceInstructions();
                break;

            case "RaceMinigame":
                LoadTugOWarInstructions();
                break;

            case "TugOWarMinigame 1":
                LoadBumperInstructions();
                break;

            case "BumperMinigame":
                SceneManager.LoadScene("EndingAnimatic");
                break;
        }
    }
}
