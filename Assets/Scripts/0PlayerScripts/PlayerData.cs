using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName; //store players name
    public int characterIndex; //store players character choice

    //sets players name
    public void SetPlayerName(string n) 
    { 
        playerName = n; 
    }

    //sets players character choice
    public void SetCharacter(int index) 
    { 
        characterIndex = index; 
    }
}
