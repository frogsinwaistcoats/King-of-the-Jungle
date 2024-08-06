using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public TextMeshProUGUI[] playerPrompts;
    MultiplayerInputManager inputManager;

    private void Start()
    {
        inputManager = MultiplayerInputManager.instance;
        inputManager.onPlayerJoined += UpdatePrompts;
        int joinedPlayers = inputManager.players.Count;
        for(int i = 0; i < joinedPlayers; i++)
        {
            UpdatePrompts(i);
        }
    }

    public void UpdatePrompts(int id)
    {
        playerPrompts[id].text = "Choose Character";
    }

    public void ContinueToGame()
    {
        SceneManager.LoadScene("MinigameMaze");
    }
}
