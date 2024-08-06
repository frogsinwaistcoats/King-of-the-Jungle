using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerDodgeball2 : MonoBehaviour
{
    public void EndGame()
    {
        // Implement your end game logic here
        Debug.Log("Game Over!");
        // You can add more functionality here, like loading a game over screen, stopping the game, etc.
        SceneManager.LoadScene("BumperMinigame");
    }
}
