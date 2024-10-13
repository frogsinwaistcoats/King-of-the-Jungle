using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndingAnimaticManager : MonoBehaviour
{
    private void Update()
    {
        Gamepad gamepad = Gamepad.current;

        if (SceneManager.GetActiveScene().name == "EndingAnimatic")
        {
            if (Input.GetKey(KeyCode.Space) || (gamepad != null && gamepad.buttonSouth.isPressed))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
