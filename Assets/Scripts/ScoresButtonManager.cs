using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoresButtonManager : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    ScoreManager scoreManager;
    public Button[] buttons;
    public Button minigameButton;

    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        scoreManager = GetComponent<ScoreManager>();

        if (sceneLoader.storyMode)
        {
            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(false);
            }

            minigameButton.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
        }
        else if (!sceneLoader.storyMode)
        {
            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(true);
            }

            minigameButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play Again";
        }
    }

    private void Start()
    {
        SetButtonsInteractable(false);
        StartCoroutine(EnableButtons(3f));
    }

    IEnumerator EnableButtons(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetButtonsInteractable(true);
    }

    private void SetButtonsInteractable(bool isInteractable)
    {
        foreach (Button button in buttons)
        {
            button.interactable = isInteractable;
        }

        minigameButton.interactable = isInteractable;
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
            if (sceneLoader.storyMode)
            {
                sceneLoader.LoadTotalScores();
            }
            else if (!sceneLoader.storyMode)
            {
                sceneLoader.LoadPreviousScene();
            }

            scoreManager.ResetScore();
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

    public void NextClick()
    {
        if (sceneLoader != null)
        {
            scoreManager.ResetScore();
            sceneLoader.LoadTotalScores();
        }
        else
        {
            Debug.LogError("Scene loader instance not found");
        }
    }
}
