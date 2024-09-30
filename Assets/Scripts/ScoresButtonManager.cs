using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoresButtonManager : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    ScoreManager scoreManager;
    public GameObject[] buttons;
    public GameObject minigameButton;

    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        scoreManager = GetComponent<ScoreManager>();

        if (sceneLoader.storyMode)
        {
            foreach (GameObject button in buttons)
            {
                button.SetActive(false);
            }

            minigameButton.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
        }
        else if (!sceneLoader.storyMode)
        {
            foreach (GameObject button in buttons)
            {
                button.SetActive(true);
            }

            minigameButton.GetComponentInChildren<Text>().text = "Play Again";
        }
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
