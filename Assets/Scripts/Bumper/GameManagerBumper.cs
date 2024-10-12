using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManagerBumper : MonoBehaviour
{
   
    public GameObject Boundary;

    public static GameManagerBumper instance;

    public TextMeshProUGUI[] playerFinishText;
    public TextMeshProUGUI gameFinishText;
    private static int finishedPlayers = 0;
    private static int totalPlayers;

    GameManager gameManager;
    SceneLoader sceneLoader;
    BumperTimer timer;
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("has entered");
        if (other.tag == "Player" )
        {
            Destroy(other.gameObject);
        }

    }
    private void Awake()
    {
        instance = this;
        gameManager = GameManager.instance;
        sceneLoader = SceneLoader.instance;
        timer = FindObjectOfType<BumperTimer>();

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
        Debug.Log("Finished players: " + finishedPlayers);

        if (finishedPlayers == (totalPlayers - 1) || finishedPlayers == (totalPlayers))
        {
            GameFinish();
        }

        return finishedPlayers;
    }

    public void GameFinish()
    {
        sceneLoader.SetPreviousScene();
        timer.CancelTimer();
        gameFinishText.enabled = true;
        StartCoroutine(NextScene());
    }

   
    IEnumerator NextScene()
    {
        //GameFinish(); 
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Scores");
    }


}