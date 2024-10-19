using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Linq;

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
        AudioManager.instance.StopAllSounds();
        AudioManager.instance.Play("AnimaticMusic");
    }

    public void LoadMainMenu()
    {
        SetPreviousScene();

        storyMode = false; //Added this in because it wasn't resetting after finishing story mode

        gameManager.ResetInstance();
        inputManager.ResetInstance();
        SceneManager.LoadScene("MainMenu");

        AudioManager.instance.StopAllSounds();
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
        AudioManager.instance.StopAllSounds();
        AudioManager.instance.Play("MazeTheme");
    }

    public void LoadRaceMinigame()
    {
        SetPreviousScene();
        SceneManager.LoadScene("RaceMinigame");
        AudioManager.instance.StopAllSounds();
        AudioManager.instance.Play("RaceTheme");
    }

    public void LoadDodgeballMinigame()
    {
        SetPreviousScene();
        SceneManager.LoadScene("DodgeballMinigame");
        AudioManager.instance.StopAllSounds();
        AudioManager.instance.Play("RaceTheme");
    }

    public void LoadBumperMinigame()
    {
        SetPreviousScene();
        SceneManager.LoadScene("BumperMinigame");
        AudioManager.instance.StopAllSounds();
        AudioManager.instance.Play("RaceTheme");
    }

    public void LoadTugOWarMinigame()
    {
        SetPreviousScene();
        SceneManager.LoadScene("TugOWarMinigame 1");
        AudioManager.instance.StopAllSounds();
        AudioManager.instance.Play("MazeTheme");
    }
    #endregion



    //  Load Instructions
    #region load instructions
    public void LoadMazeInstructions()
    {
        SetPreviousScene();
        SceneManager.LoadScene("MazeInstructionScreen");

        AudioManager.instance.StopAllSounds();
        // Play instruction music when loading instructions
        AudioManager.instance.Play("InterludeTheme");
    }

    public void LoadRaceInstructions()
    {
        SetPreviousScene();
        SceneManager.LoadScene("RaceInstructionScreen");

        AudioManager.instance.StopAllSounds();
        // Play instruction music when loading instructions
        AudioManager.instance.Play("InterludeTheme");
    }

    public void LoadDodgeballInstructions()
    {
        SetPreviousScene();
        SceneManager.LoadScene("DodgeballInstructionScreen");

        AudioManager.instance.StopAllSounds();
        // Play instruction music when loading instructions
        AudioManager.instance.Play("InterludeTheme");
    }

    public void LoadBumperInstructions()
    {
        SetPreviousScene();
        SceneManager.LoadScene("BumperInstructionScreen");

        AudioManager.instance.StopAllSounds();
        // Play instruction music when loading instructions
        AudioManager.instance.Play("InterludeTheme");
    }

    public void LoadTugOWarInstructions()
    {
        SetPreviousScene();
        SceneManager.LoadScene("TugOWarInstructionScreen");

        AudioManager.instance.StopAllSounds();
        // Play instruction music when loading instructions
        AudioManager.instance.Play("InterludeTheme");
    }
    #endregion

    public void LoadTotalScores()
    {
        SceneManager.LoadScene("TotalScores");
        //AudioManager.instance.StopAllSounds();
        //AudioManager.instance.Play("InterludeTheme");
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
