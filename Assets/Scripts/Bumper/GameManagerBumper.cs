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

    public void GameFinish()
    {
        sceneLoader.SetPreviousScene();
        timer.CancelTimer();
        gameFinishText.enabled = true;

        StartCoroutine(NextScene());
    }

   
    IEnumerator NextScene()
    {

        GameFinish(); 
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Scores");
    }


}