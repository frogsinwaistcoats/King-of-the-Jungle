using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource mainTheme;
    public AudioSource instructionMusic;
    public AudioSource gameMusic;
    public AudioSource raceMusic;
    public AudioSource dashSFX;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to play a specific audio clip
    public void Play(string name)
    {
        switch (name)
        {
            case "MainTheme":
                if (mainTheme != null)
                    mainTheme.Play();
                else
                    Debug.LogError("MainTheme AudioSource is missing!");
                break;
            case "RaceTheme":
                if (raceMusic != null)
                    raceMusic.Play();
                else
                    Debug.LogError("Race Music AudioSource is missing!");
                break;
            case "GameTheme":
                if (gameMusic != null)
                    gameMusic.Play();
                else
                    Debug.LogError("Game Music AudioSource is missing!");
                break;
            case "InstructionMusic":
                if (instructionMusic != null)
                    instructionMusic.Play();
                else
                    Debug.LogError("InstructionMusic AudioSource is missing!");
                break;
            case "Dash":
                if (dashSFX != null)
                    dashSFX.Play();
                else
                    Debug.LogError("DashSFX AudioSource is missing");
                break;
            default:
                Debug.LogError("Unknown audio name: " + name);
                break;
        }
    }

    // Method to stop playing all audio
    public void StopAllAudio()
    {
        if (mainTheme.isPlaying) mainTheme.Stop();
        if (instructionMusic.isPlaying) instructionMusic.Stop();
    }
}
