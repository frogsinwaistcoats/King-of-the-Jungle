using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinishRace : MonoBehaviour
{
    public TMPro.TextMeshProUGUI finishText;

    private void Awake()
    {
        finishText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        finishText.enabled = true;
        StartCoroutine(nextScene());
    }

    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Scores");
    }

}