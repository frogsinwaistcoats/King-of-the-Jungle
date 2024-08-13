using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

[System.Serializable]
public class IndividualPlayerControls
{

    public int playerID;
    public int characterID;
    public Sprite[] characterSprites;
    public InputDevice inputDevice;
    InputUser inputUser;

    public InputControls playerControls;
    public ControllerType controllerType;

    //to change on-screen prompts
    public enum ControllerType
    {
        Keyboard,
        Playstation,
        Xbox,
        Switch,
    }

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
        else if (inputDevice is UnityEngine.InputSystem.Keyboard)
        {
            controllerType = ControllerType.Keyboard;
        }
    }

    public void DisableControls()
    {
        playerControls.Disable();
    }
}
