using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameSelectionButtons : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void StoryMode()
    {
        if (sceneLoader != null)
        {
            sceneLoader.LoadStoryMode();
        }
        else
        {
            Debug.LogError("Scene loader instance not found");
        }
    }

    public void MazeMinigame()
    {
        if (sceneLoader != null)
        {
            sceneLoader.LoadMazeInstructions();
        }
        else
        {
            Debug.LogError("Scene loader instance not found");
        }
    }

    public void RaceMinigame()
    {
        if (sceneLoader != null)
        {
            sceneLoader.LoadRaceInstructions();
        }
        else
        {
            Debug.LogError("Scene loader instance not found");
        }
    }
    public void BumperMinigame()
    {
        if (sceneLoader != null)
        {
            sceneLoader.LoadBumperInstructions();
        }
        else
        {
            Debug.LogError("Scene loader instance not found");
        }
    }
    public void TugOWarMinigame()
    {
        if (sceneLoader != null)
        {
            sceneLoader.LoadTugOWarInstructions();
        }
        else
        {
            Debug.LogError("Scene loader instance not found");
        }
    }
    public void DodgeballMinigame()
    {
        if (sceneLoader != null)
        {
            sceneLoader.LoadDodgeballInstructions();
        }
        else
        {
            Debug.LogError("Scene loader instance not found");
        }
    }

}
