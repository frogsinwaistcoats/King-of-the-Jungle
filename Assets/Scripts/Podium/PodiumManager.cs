using System.Linq;
using UnityEngine;

public class PodiumManager : MonoBehaviour
{
    public Transform[] podiumPositions; // Assign these in the inspector for 1st, 2nd, 3rd, 4th
    public GameObject podiumPlayerPrefab; // Prefab to display the player (empty game object with a SpriteRenderer)

    private void Start()
    {
        //debug
        foreach (PlayerData player in GameManager.instance.players)
        {
            Debug.Log($"Player {player.playerID}: Character Sprite = {player.characterSprite.name}");
        }

        PlacePlayersOnPodium();
    }

    private void PlacePlayersOnPodium()
    {
        // Get players ordered by score
        var orderedPlayers = GameManager.instance.players.OrderByDescending(p => p.totalScore).ToList();

        for (int i = 0; i < orderedPlayers.Count && i < podiumPositions.Length; i++)
        {
            PlayerData playerData = orderedPlayers[i];

            // Instantiate a prefab at the podium position
            GameObject podiumPlayer = Instantiate(podiumPlayerPrefab, podiumPositions[i].position, podiumPositions[i].rotation);

            // Assign the player's sprite to the prefab's SpriteRenderer
            SpriteRenderer spriteRenderer = podiumPlayer.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = playerData.characterSprite;
            }
            else
            {
                Debug.LogError("Podium player prefab is missing a SpriteRenderer!");
            }
        }
    }
}