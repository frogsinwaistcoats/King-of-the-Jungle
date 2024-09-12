using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void MainMenuClick()
    {
        if (sceneLoader != null)
        {
            sceneLoader.LoadMainMenu();
        }
        else
        {
            Debug.LogError("Scene loader instance not found");
        }
    }
}
