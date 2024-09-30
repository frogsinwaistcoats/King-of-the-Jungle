using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalScoresButtonManager : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    ScoreManager scoreManager;

    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        scoreManager = GetComponent<ScoreManager>();
    }

    public void NextClick()
    {
        if (sceneLoader != null)
        {
            sceneLoader.LoadInstructions();
        }
        else
        {
            Debug.LogError("Scene loader instance not found");
        }
    }
}
