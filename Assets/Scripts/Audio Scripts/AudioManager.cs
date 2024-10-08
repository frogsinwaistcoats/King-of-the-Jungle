using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    //public AudioSource mainTheme;
    //public AudioSource instructionMusic;
    //public AudioSource gameMusic;
    //public AudioSource raceMusic;

    private Dictionary<string, AudioSource> soundEffects = new Dictionary<string, AudioSource>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSoundEffects();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSoundEffects()
    {
        // Add each sound effect to the dictionary and load from Resources folder
        soundEffects.Add("Shoot1", gameObject.AddComponent<AudioSource>());
        soundEffects["Shoot1"].clip = Resources.Load<AudioClip>("Audio/SFX/Dodgeball/Whoosh_high");

        soundEffects.Add("Shoot2", gameObject.AddComponent<AudioSource>());
        soundEffects["Shoot2"].clip = Resources.Load<AudioClip>("Audio/SFX/Dodgeball/Whoosh_low");

        soundEffects.Add("Dash", gameObject.AddComponent<AudioSource>());
        soundEffects["Dash"].clip = Resources.Load<AudioClip>("Audio/SFX/Dodgeball/Woosh_dash");

        soundEffects.Add("Hit", gameObject.AddComponent<AudioSource>());
        soundEffects["Hit"].clip = Resources.Load<AudioClip>("Audio/SFX/Dodgeball/Hit");

        soundEffects.Add("Plop", gameObject.AddComponent<AudioSource>());
        soundEffects["Plop"].clip = Resources.Load<AudioClip>("Audio/SFX/Dodgeball/Water_plop");

        soundEffects.Add("Splat", gameObject.AddComponent<AudioSource>());
        soundEffects["Splat"].clip = Resources.Load<AudioClip>("Audio/SFX/Dodgeball/Splat");

        soundEffects.Add("RiverSounds", gameObject.AddComponent<AudioSource>());
        soundEffects["RiverSounds"].clip = Resources.Load<AudioClip>("Audio/SFX/Dodgeball/River_Sounds");
        soundEffects["RiverSounds"].loop = true;  // Set looping if needed

        soundEffects.Add("PartyHorn", gameObject.AddComponent<AudioSource>());
        soundEffects["PartyHorn"].clip = Resources.Load<AudioClip>("Audio/SFX/Dodgeball/Party Horn");

        soundEffects.Add("Woohoo", gameObject.AddComponent<AudioSource>());
        soundEffects["Woohoo"].clip = Resources.Load<AudioClip>("Audio/SFX/Dodgeball/woohoo");

        soundEffects.Add("Fireworks", gameObject.AddComponent<AudioSource>());
        soundEffects["Fireworks"].clip = Resources.Load<AudioClip>("Audio/SFX/Dodgeball/Fireworks_short");
        soundEffects["Fireworks"].loop = true;  // Set looping if needed

        // Music
        //soundEffects.Add("CelebrationMusic", gameObject.AddComponent<AudioSource>());
        //soundEffects["CelebrationMusic"].clip = Resources.Load<AudioClip>("Audio/Music/CelebrationMusic");
        //soundEffects["CelebrationMusic"].loop = true;  // Set looping if needed

        soundEffects.Add("MainTheme", gameObject.AddComponent<AudioSource>());
        soundEffects["MainTheme"].clip = Resources.Load<AudioClip>("Audio/Music/Jungle King Main Theme");
        soundEffects["MainTheme"].loop = true;

        soundEffects.Add("InterludeTheme", gameObject.AddComponent<AudioSource>());
        soundEffects["InterludeTheme"].clip = Resources.Load<AudioClip>("Audio/Music/Interlude Theme");
        soundEffects["InterludeTheme"].loop = true;

    }


    public void Play(string soundName)
    {
        if (soundEffects.ContainsKey(soundName))
        {
            soundEffects[soundName].Play();
        }
        else
        {
            Debug.LogError("Sound not found: " + soundName);
        }
    }
     // Method to stop a sound by name
    public void Stop(string soundName)
    {
        if (soundEffects.ContainsKey(soundName) && soundEffects[soundName].isPlaying)
        {
            soundEffects[soundName].Stop();
        }
    }

    // Stop all sound effects
    public void StopAllSounds()
    {
        foreach (var soundEffect in soundEffects.Values)
        {
            if (soundEffect.isPlaying) soundEffect.Stop();
        }
    }
}
