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

    private void Awake()
    {
        instance = this;
        gameManager = GameManager.instance;

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

    public void PlayerFinish(int id)
    {
        finishedPlayers++;
        playerFinishText[id].enabled = true;
        Debug.Log("Finished players: " + finishedPlayers);
        
        //playerFinishText.enabled = true;
        //if ()
        //{
        //    playerFinishText.text = "1st";
        //}
        //else if ()
        //{
        //    playerFinishText.text = "2nd";
        //}
        //else if ()
        //{
        //    playerFinishText.text = "3rd";
        //}

        if (finishedPlayers == (totalPlayers - 1) || finishedPlayers == (totalPlayers))
        {
            gameFinishText.enabled = true;
            StartCoroutine(NextScene());
            Debug.Log("Next scene");
        }
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Scores");
    }
}
