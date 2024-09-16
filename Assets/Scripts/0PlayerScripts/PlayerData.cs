using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

[System.Serializable]
public class PlayerData
{
    public int playerID;
    public int characterIndex;
    public ControllerType controllerType;
    public float playerScore;
    public float totalScore;

    public void SetPlayerID(int n)
    {
        playerID = n;
    }

    public void SetCharacter(int index)
    {
        characterIndex = index;
    }

    public void SetControllerType(ControllerType t)
    {
        controllerType = t;
    }

    public void SetPlayerScore(float score)
    {
        playerScore += score;
    }

    public void SetTotalScore(float score)
    {
        totalScore += score;
    }
}
