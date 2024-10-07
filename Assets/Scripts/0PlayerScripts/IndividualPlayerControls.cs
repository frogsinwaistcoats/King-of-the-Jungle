using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

[System.Serializable]
public class IndividualPlayerControls
{

    public int playerID;
    public InputDevice inputDevice;
    InputUser inputUser;

    public InputControls playerControls;
    public ControllerType controllerType;

    //to change on-screen prompts
    

    public void SetupPlayer(InputAction.CallbackContext obj, int ID)
    {
        playerID = ID;
        inputDevice = obj.control.device;

        playerControls = new InputControls();
        //this section is how we assign a single device to the inputs
        inputUser = InputUser.PerformPairingWithDevice(inputDevice);
        inputUser.AssociateActionsWithUser(playerControls);

        playerControls.Enable();
        SetControllerType();

        GameManager.instance.AddPlayer(new PlayerData(), playerID, 0, "", controllerType, null);
    }

    void SetControllerType()
    {
        if (inputDevice is UnityEngine.InputSystem.DualShock.DualShockGamepad)
        {
            controllerType = ControllerType.Playstation;
        }
        else if (inputDevice is UnityEngine.InputSystem.XInput.XInputControllerWindows)
        {
            controllerType = ControllerType.Xbox;
        }
        else if (inputDevice is UnityEngine.InputSystem.Switch.SwitchProControllerHID)
        {
            controllerType = ControllerType.Switch;
        }
        else if (inputDevice is Keyboard)
        {
            controllerType = ControllerType.Keyboard;
        }

        if (playerID == 0)
        {
            GameManager.instance.player1ControllerType = controllerType;
        }
    }

    public void DisableControls()
    {
        playerControls.Disable();
    }
}
