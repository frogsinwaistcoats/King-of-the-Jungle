using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TOWFinish : MonoBehaviour
{
    public static TOWFinish instance;
    GameManager gameManager;
    SceneLoader sceneLoader;
    public TextMeshProUGUI gameOverText;

    private void Awake()
    {
        instance = this;

        sceneLoader = SceneLoader.instance;
        gameManager = GameManager.instance;
    }

    private void Start()
    {
        gameOverText.enabled = false;
    }

    public void GameOver()
    {
        Animator[] animators = FindObjectsOfType<Animator>();
        foreach (Animator animator in animators)
        {
            animator.enabled = false;
        }
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
