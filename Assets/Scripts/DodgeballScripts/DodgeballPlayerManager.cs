using System.Collections.Generic;
using UnityEngine;

public class DodgeballPlayerManager : MonoBehaviour
{
    public static DodgeballPlayerManager instance;
    public List<GameObject> playerPrefabs = new List<GameObject>();
    public List<int> selectedPrefabsIndex = new List<int>();
    public List<GameObject> spawnedPlayers = new List<GameObject>(); // Store spawned players

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this from character selection
    public void SetPlayerPrefab(int playerID, int prefabIndex)
    {
        if (playerID >= selectedPrefabsIndex.Count)
        {
            selectedPrefabsIndex.Add(prefabIndex);
        }
        else
        {
            selectedPrefabsIndex[playerID] = prefabIndex;
        }
    }

    // Instantiate players at the start of the minigame
    public void SpawnPlayers(Vector3[] spawnPositions)
    {
        for (int i = 0; i < selectedPrefabsIndex.Count; i++)
        {
            GameObject player = Instantiate(playerPrefabs[selectedPrefabsIndex[i]], spawnPositions[i], Quaternion.identity);
            spawnedPlayers.Add(player);
        }
    }

    // Enable/Disable controls for all players
    public void SetPlayerControls(bool enable)
    {
        foreach (GameObject player in spawnedPlayers)
        {
            PlayerAiming aiming = player.GetComponent<PlayerAiming>();
            DodgeballPlayerMovement movement = player.GetComponent<DodgeballPlayerMovement>();

            if (aiming != null)
                aiming.enabled = enable; // Enable or disable aiming

            if (movement != null)
                movement.enabled = enable; // Enable or disable movement
        }
    }
}