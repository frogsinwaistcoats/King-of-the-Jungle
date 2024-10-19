using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSceneAudio : MonoBehaviour
{
    private void Start()
    {
        // Stop any instruction music if it's playing
        AudioManager.instance.StopAllSounds();

        // Play main theme when entering the main menu
        AudioManager.instance.Play("Fireworks");
        AudioManager.instance.Play("CelebrationMusic");
    }
}
