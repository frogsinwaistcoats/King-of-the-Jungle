using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    MultiplayerInputManager inputManager;

    GameManager gameManager;

    private void Awake()
    {
        inputManager = MultiplayerInputManager.instance;
        gameManager = GameManager.instance;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //inputManager.inputControls.MasterControls.NextButton.performed += NextButton_performed;
    }
    


    //private void NextButton_performed(InputAction.CallbackContext obj)
    //{
    //    if (SceneManager.GetActiveScene().name == "MainMenu")
    //    {
    //        LoadCharacterSelection();
    //    }

    //    if (SceneManager.GetActiveScene().name == "CharacterSelection 1")
    //    {
    //        if (inputManager.players.Count >= 2) 
    //        {
    //            LoadMinigameSelection();
    //        }
    //        else
    //        {
    //            Debug.Log("Not enough Players");
    //        }
    //    }
    //}
    
    public void LoadMainMenu()
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

    //Load Minigames
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

    //Load Instructions
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
