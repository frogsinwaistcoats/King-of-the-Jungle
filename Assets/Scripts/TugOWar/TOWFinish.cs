using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TOWFinish : MonoBehaviour
{
    public static TOWFinish instance;
    SceneLoader sceneLoader;
    public TextMeshProUGUI gameOverText;

    private void Awake()
    {
        instance = this;

        sceneLoader = SceneLoader.instance;
    }

    private void Start()
    {
        gameOverText.enabled = false;
    }

    public void GameOver()
    {
        TOWTimer.instance.CancelTimer();
        gameOverText.enabled = true;
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(3);
        sceneLoader.SetPreviousScene();
        SceneManager.LoadScene("Scores");
    }
}
