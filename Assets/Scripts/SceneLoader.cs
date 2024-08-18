using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    MultiplayerInputManager inputManager;

    private void Awake()
    {
        inputManager = MultiplayerInputManager.instance;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        inputManager.inputControls.MasterControls.NextButton.performed += NextButton_performed;
    }

    public void LoadCharacterSelection()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void LoadMinigameSelection()
    {
        SceneManager.LoadScene("MinigameSelection");
    }

    private void NextButton_performed(InputAction.CallbackContext obj)
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            LoadCharacterSelection();
        }

        if (SceneManager.GetActiveScene().name == "CharacterSelection")
        {
            if (inputManager.players.Count >= 2) 
            {
                LoadMinigameSelection();
            }
            else
            {
                Debug.Log("Not enough Players");
            }
        }
    }

    public void PlayMazeMinigame()
    {
        SceneManager.LoadScene("MazeMinigame");
    }

    public void PlayRaceMinigame()
    {
        SceneManager.LoadScene("RaceMinigame");
    }

    public void PlayDodgeballMinigame()
    {
        SceneManager.LoadScene("DodgeballMinigame");
    }

    public void PlayBumperMinigame()
    {
        SceneManager.LoadScene("BumperMinigame");
    }

    public void PlayTugOWarMinigame()
    {
        SceneManager.LoadScene("TugOWarMinigame");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadPreviousScene()
    {
        int currentScene;
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene - 1);
    }
}
