using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    private void Start()
    {
        // Stop any instruction music if it's playing
        AudioManager.instance.Stop("InterludeTheme");

        // Play main theme when entering the main menu
        AudioManager.instance.Play("MainTheme");
    }
}
