using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // For button handling

public class EndingAnimaticManager : MonoBehaviour
{
    public Button mainMenuButton; // Assign this in the inspector

    private void Start()
    {
        // Add a listener for the main menu button click
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
    }

    private void Update()
    {
        Gamepad gamepad = Gamepad.current;

        // Detect Spacebar or Gamepad South button press to return to MainMenu
        if (Input.GetKey(KeyCode.Space) || (gamepad != null && gamepad.buttonSouth.isPressed))
        {
            ReturnToMainMenu();  // Call the function to go back to main menu
        }
    }

    // Function to handle returning to the main menu
    private void ReturnToMainMenu()
    {
        // Stop any sounds
        AudioManager.instance.StopAllSounds();

        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
