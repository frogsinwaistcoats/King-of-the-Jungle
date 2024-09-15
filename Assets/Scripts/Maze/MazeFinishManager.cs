using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MazeFinishManager : MonoBehaviour
{
    public static MazeFinishManager instance;

    public TextMeshProUGUI[] playerFinishText;
    public TextMeshPro gameFinishText;

    private static int finishedPlayers = 0;
    private static int totalPlayers;

    GameManager gameManager;
    SceneLoader sceneLoader;

    private void Awake()
    {
        instance = this;
        gameManager = GameManager.instance;
        sceneLoader = SceneLoader.instance;

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
        playerFinishText[id].text = finishedPlayers + " !";
        Debug.Log("Finished players: " + finishedPlayers);

        if (finishedPlayers == (totalPlayers - 1) || finishedPlayers == (totalPlayers))
        {
            gameFinishText.enabled = true;
            StartCoroutine(NextScene());
        }

        return finishedPlayers;
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
