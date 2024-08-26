using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int characterIndex;

    public void SetPlayerName(string n)
    {
        playerName = n;
    }

    public void SetCharacter(int index)
    {
        characterIndex = index;
    }
}
