using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public InputControls inputControls;
    MultiplayerInputManager inputManager;

    public int playerID;


    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);

        inputManager = MultiplayerInputManager.instance;
        if (inputManager.players.Count >= playerID + 1)
        {
            AssignInputs(playerID);
        }
        else
        {
            inputManager.onPlayerJoined += AssignInputs;
        }
    }

    public void AssignInputs(int ID)
    {
        if (ID == playerID)
        {
            inputManager.onPlayerJoined += AssignInputs;

            if (inputManager.players.Count > ID)
            {
                inputControls = inputManager.players[playerID].playerControls;
                //inputControls.UIControls.Move.performed += Move;
                inputControls.UIControls.Submit.performed += OnSubmit;
            }
        }
    }

    /*
    private void Move(InputAction.CallbackContext obj)
    {
        Vector2 moveInput = obj.ReadValue<Vector2>();

        if (moveInput != Vector2.zero)
        {
            if (EventSystem.current != null)
            {
                EventSystem.current.SendMessage("Move", moveInput);
            }
        }
    }
    */

    private void OnSubmit(InputAction.CallbackContext obj)
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
    }

    public void OnDisable()
    {
        if (inputControls != null)
        {
            //inputControls.UIControls.Move.performed -= Move;
            inputControls.UIControls.Submit.performed -= OnSubmit;
        }
        else
        {
            inputManager.onPlayerJoined -= AssignInputs;
        }
    }
}
