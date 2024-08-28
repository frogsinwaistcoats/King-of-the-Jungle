using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int playerID;
    public int characterIndex;

    public void SetPlayerID(int n)
    {
        playerID = n;
    }

    public void SetCharacter(int index)
    {
        characterIndex = index;
    }
}
