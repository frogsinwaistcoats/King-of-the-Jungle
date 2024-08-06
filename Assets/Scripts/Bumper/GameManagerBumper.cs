using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerBumper : MonoBehaviour
{
    public GameObject[] players;
    public float arenaRadius = 10f;
    public TextMeshProUGUI gameStatusText;

    void Update()
    {
        foreach (GameObject player in players)
        {
            if (Vector3.Distance(player.transform.position, Vector3.zero) > arenaRadius)
            {
                gameStatusText.text = player.name + " has fallen out!";
                Destroy(player);
                CheckGameOver();
            }
        }
    }

    void CheckGameOver()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length <= 1)
        {
            gameStatusText.text = "Game Over";
        }
    }
}