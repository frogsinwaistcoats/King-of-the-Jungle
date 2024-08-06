using UnityEngine;
using UnityEngine.InputSystem; //Used for the unity's new input system
using UnityEngine.InputSystem.Users; //Used to set different devices for inputs - multiplayer

[System.Serializable]
public class IndividualDevice
{
    [field: SerializeField] public int playerID { get; private set; }
    [field: SerializeField] public string deviceName { get; private set; }
    [field: SerializeField] public InputDevice inputDevice { get; private set; }
    public InputControls playerControls { get; private set; }
    InputUser inputUser;

    [field: SerializeField] public ControllerType controllerType { get; private set; }
    public enum ControllerType
    {
        MissingDevice,
        Playstation,
        Xbox,
        Switch,
        Keyboard
    }

    public void SetUpPlayer(InputAction.CallbackContext obj, int ID)
    {
        playerID = ID;
        inputDevice = obj.control.device;
        deviceName = inputDevice.displayName;

        playerControls = new InputControls();
        //This section is how we assign a single device to the inputs
        inputUser = InputUser.PerformPairingWithDevice(inputDevice); //Pairs the device with this user
        inputUser.AssociateActionsWithUser(playerControls); //pairs the user with this current inputs
        //
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
            //You may need to add a bit more here if you also want to assign the mouse to the user
        }
        else
        {
            //Use default Xbox
            controllerType = ControllerType.Xbox;
        }
    }

    public void DisableControls()
    {
        playerControls.Disable();
    }
}