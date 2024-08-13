using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public List<GameObject> playerPrefabs = new List<GameObject>(); // Assign these in the inspector
    public List<int> selectedPrefabsIndex = new List<int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
}
