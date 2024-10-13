using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource tickSource;
    private void Start()
    {
        tickSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            tickSource.Play();
        }
    }
}
