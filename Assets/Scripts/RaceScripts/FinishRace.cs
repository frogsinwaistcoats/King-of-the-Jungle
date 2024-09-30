using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinishRace : MonoBehaviour
{
    public static FinishRace instance;

    public TextMeshProUGUI finishText;

    private static int finishedPlayers = 0;
    private static int totalPlayers;

    GameManager gameManager;
    SceneLoader sceneLoader;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        sceneLoader = SceneLoader.instance;

        finishText.enabled = false;
        totalPlayers = gameManager.players.Count;

    }

    public int PlayerFinish()
    {
        finishedPlayers++;

        if (finishedPlayers == (totalPlayers - 1) || finishedPlayers == (totalPlayers))
        {
            RaceGameFinish();
        }

        return finishedPlayers;
    }

    public void RaceGameFinish()
    {
        finishText.enabled = true;
        StartCoroutine(NextScene());
    }

    public int CalculateScore(int placing)
    {
        int score = 0;

        if (placing == 1)
        {
            score = totalPlayers - 1;
        }
        else if (placing == 2)
        {
            score = totalPlayers - 2;
        }
        else if (placing == 3)
        {
            score = totalPlayers - 3;
        }
        else if (placing == 4)
        {
            score = totalPlayers - 4;
        }

        return score;
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(3);
        sceneLoader.SetPreviousScene();
        finishedPlayers = 0;
        SceneManager.LoadScene("Scores");
    }

}