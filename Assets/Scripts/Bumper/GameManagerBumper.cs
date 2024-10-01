using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManagerBumper : MonoBehaviour
{
   [SerializeField] GameObject Player;
    public GameObject Boundary;

    public static GameManagerBumper instance;

    public TextMeshProUGUI[] playerFinishText;
    public TextMeshProUGUI gameFinishText;

    private static int finishedPlayers = 0;
    private static int totalPlayers;

    GameManager gameManager;
    SceneLoader sceneLoader;
    TimerBumper _timeBump;
    

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

        if ( sceneLoader == null)
        {
            sceneLoader = FindAnyObjectByType<SceneLoader>();
        }

       _timeBump = FindObjectOfType<TimerBumper>();

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
            GameFinish();
        }

        return finishedPlayers;
    }

    public void GameFinish()
    {
        sceneLoader.SetPreviousScene();
        _timeBump.CancelTimer();
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

        finishedPlayers = 0;
        SceneManager.LoadScene("Scores");
    }


}