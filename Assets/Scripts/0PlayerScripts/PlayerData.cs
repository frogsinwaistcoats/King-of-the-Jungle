using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

[System.Serializable]
public class PlayerData
{
    public int playerID;
    public int characterIndex;
    public ControllerType controllerType;

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
}
