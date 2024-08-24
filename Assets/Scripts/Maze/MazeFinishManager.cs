using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MazeFinishManager : MonoBehaviour
{
    public TextMeshPro playerFinishText;
    public TextMeshPro gameFinishText;

    private static int finishedPlayers = 0;
    private static int totalPlayers;

    public MultiplayerInputManager inputManager;

    private void Awake()
    {
        playerFinishText.enabled = false;
        gameFinishText.enabled = false;
        inputManager = MultiplayerInputManager.instance;
    }

    private void Start()
    {
        totalPlayers = inputManager.PlayerCount;
        Debug.Log("Number of players: " + totalPlayers);
    }

    private void OnTriggerEnter(Collider other)
    {
        finishedPlayers++;
        Debug.Log("Finished players: " + finishedPlayers);
        playerFinishText.enabled = true;
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
