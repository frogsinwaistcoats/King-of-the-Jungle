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

    public void LoadMainMenu()
    {
        gameManager.ResetInstance();
        inputManager.ResetInstance();
        SceneManager.LoadScene("MainMenu");
        //SceneManager.UnloadSceneAsync("ManagerScene").completed += OnManagerSceneUnloaded;

    }

    private void OnManagerSceneUnloaded(AsyncOperation obj)
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadCharacterSelection()
    {
        SceneManager.LoadScene("CharacterSelection");
    }


    public void LoadMinigameSelection()
    {

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
        //int currentScene;
        //currentScene = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(currentScene - 1);
    }

    //  Load Minigames
    #region load minigames
    public void LoadMazeMinigame()
    {
        SceneManager.LoadScene("MazeMinigame");
    }

    public void LoadRaceMinigame()
    {
        SceneManager.LoadScene("RaceMinigame");
    }

    public void LoadDodgeballMinigame()
    {
        SceneManager.LoadScene("DodgeballMinigame");
    }

    public void LoadBumperMinigame()
    {
        SceneManager.LoadScene("BumperMinigame");
    }

    public void LoadTugOWarMinigame()
    {
        SceneManager.LoadScene("TugOWarMinigame 1");
    }
    #endregion

    public void LoadMinigame()
    {
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

    //  Load Instructions
    #region load instructions
    public void LoadMazeInstructions()
    {
        SceneManager.LoadScene("MazeInstructionScreen");
    }

    public void LoadRaceInstructions()
    {
        SceneManager.LoadScene("RaceInstructionScreen");
    }

    public void LoadDodgeballInstructions()
    {
        SceneManager.LoadScene("DodgeballInstructionScreen");
    }

    public void LoadBumperInstructions()
    {
        SceneManager.LoadScene("BumperInstructionScreen");
    }

    public void LoadTugOWarInstructions()
    {
        SceneManager.LoadScene("TugOWarInstructionScreen");
    }
    #endregion

}
