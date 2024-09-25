using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadSceneAsync("ManagerScene", LoadSceneMode.Additive);
    }

    private void Update()
    {
        Gamepad gamepad = Gamepad.current;

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (Input.GetKey(KeyCode.Space) || (gamepad!=null && gamepad.buttonSouth.isPressed))
            {
                SceneManager.LoadScene("CharacterSelection");
            }
        }
    }
}
