using System.Linq;
using UnityEngine;

public class PodiumManager : MonoBehaviour
{
    public Transform[] podiumPositions; // Assign the podium spawn points (1st, 2nd, 3rd, 4th) in the inspector
    private DodgeballPlayerManager playerManager;
    private GameManager gameManager;

    private void Start()
    {
        playerManager = DodgeballPlayerManager.instance;
        gameManager = GameManager.instance;

        PlacePlayersOnPodium();
    }

    private void PlacePlayersOnPodium()
    {
        // Get the players ordered by score
        var orderedPlayers = gameManager.players.OrderByDescending(p => p.playerScore).ToList();

        for (int i = 0; i < orderedPlayers.Count && i < podiumPositions.Length; i++)
        {
            // Get the corresponding player prefab index
            int prefabIndex = playerManager.selectedPrefabsIndex[orderedPlayers[i].playerID];

            // Instantiate the player prefab at the corresponding podium position
            GameObject playerPrefab = playerManager.playerPrefabs[prefabIndex];
            Instantiate(playerPrefab, podiumPositions[i].position, podiumPositions[i].rotation);
        }
    }
}
