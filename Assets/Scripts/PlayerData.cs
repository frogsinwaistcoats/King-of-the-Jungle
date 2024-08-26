using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int playerID;
    public int characterIndex;

    public void SetPlayerName(string n)
    {
        playerName = n;
    }

    public void SetPlayerID(int n)
    {
        playerID = n;
    }

    public void SetCharacter(int index)
    {
        characterIndex = index;
    }
}
