using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager1 : MonoBehaviour
{
    public Vector2 move;
    public bool jump;

    public static InputControls inputControls;

    // Start is called before the first frame update
    void Start()
    { //creates inputs
        inputControls = new InputControls();
        inputControls.MasterControls.Jump.performed += Jump_performed; //get key down
        inputControls.MasterControls.Jump.canceled += Jump_canceled; //get key up
        inputControls.MasterControls.Move.performed += Move_performed;
        inputControls.MasterControls.Move.canceled += Move_canceled;
        inputControls.Enable();
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        move = Vector2.zero;
    }


    private void Move_performed(InputAction.CallbackContext obj)//obj or context doesn't matter
    {
        move = obj.ReadValue<Vector2>(); //reads value of what you put in inputs
    }

    private void Jump_canceled(InputAction.CallbackContext obj)
    {
        jump = false;
    }
    //function for when jump button is detected (on key down)
    private void Jump_performed(InputAction.CallbackContext context)
    {
        jump = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
