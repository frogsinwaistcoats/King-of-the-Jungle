using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RaceFinishManager : MonoBehaviour
{
    public static RaceFinishManager instance;

    public TextMeshProUGUI[] playerFinishText;
    public TextMeshProUGUI gameFinishText;
    public Image panel;

    private static int finishedPlayers = 0;
    private static int totalPlayers;

    GameManager gameManager;
    SceneLoader sceneLoader;
    RaceTimer raceTimer;

    private void Awake()
    {
        instance = this;
        gameManager = GameManager.instance;
        sceneLoader = SceneLoader.instance;
        raceTimer = FindObjectOfType<RaceTimer>();

        for (int i = 0; i < playerFinishText.Length; i++)
        {
            if (playerFinishText[i] != null)
            {
                playerFinishText[i].enabled = false;
            }
        }

        gameFinishText.enabled = false;
    }

    private void Start()
    {
        totalPlayers = gameManager.players.Count;
    }

    public int PlayerFinish(int id)
    {
        finishedPlayers++;
        playerFinishText[id].enabled = true;

        if (finishedPlayers == (totalPlayers - 1) || finishedPlayers == (totalPlayers))
        {
            RaceGameFinish();
        }

        return finishedPlayers;
    }

    public void RaceGameFinish()
    {
        raceTimer.CancelTimer();
        panel.enabled = false;
        gameFinishText.enabled = true;
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