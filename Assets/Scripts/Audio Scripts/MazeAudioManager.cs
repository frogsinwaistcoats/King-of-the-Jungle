using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeAudioManager : MonoBehaviour
{
    public static MazeAudioManager instance;

    AudioSource audioSource;
    public GameObject spinPrefab;
    public GameObject winPrefab;
    public GameObject thudPrefab;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySpinSFX()
    {
        GameObject spinSFX = Instantiate(spinPrefab, transform.position, transform.rotation);
        float sfxLength = spinSFX.GetComponent<AudioSource>().clip.length;
        Destroy(spinSFX, sfxLength);
    }

    public void PlayWinSFX()
    {
        GameObject winSFX = Instantiate(winPrefab, transform.position, transform.rotation);
        float sfxLength = winSFX.GetComponent<AudioSource>().clip.length;
        Destroy(winSFX, sfxLength);
    }

    public void PlayThudSFX()
    {
        GameObject thudSFX = Instantiate(thudPrefab, transform.position, transform.rotation);
        float sfxLength = thudSFX.GetComponent<AudioSource>().clip.length;
        Destroy(thudSFX, sfxLength);
    }
}
