using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    ScoreManager scoreManager;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        scoreManager = GetComponent<ScoreManager>();
    }

    public void MainMenuClick()
    {
        if (sceneLoader != null)
        {
            scoreManager.ResetScore();
            sceneLoader.LoadMainMenu();
        }
        else
        {
            Debug.LogError("Scene loader instance not found");
        }
    }

    public void PlayAgainClick()
    {
        if (sceneLoader != null)
        {
            scoreManager.ResetScore();
            sceneLoader.LoadPreviousScene();
        }
        else
        {
            Debug.LogError("Scene loader instance not found");
        }
    }

    public void MinigameSelectClick()
    {
        if (sceneLoader != null)
        {
            scoreManager.ResetScore();
            sceneLoader.LoadMinigameSelection();
        }
        else
        {
            Debug.LogError("Scene loader instance not found");
        }
    }
}
