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

    public void PlayGame()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void NextButton_performed(InputAction.CallbackContext obj)
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            PlayGame();
        }

        if (SceneManager.GetActiveScene().name == "CharacterSelection")
        {
            if (inputManager.players.Count >= 2) 
            {
                ContinueGame();
            }
            else
            {
                Debug.Log("Not enough Players");
            }
            
        }

    }
}
