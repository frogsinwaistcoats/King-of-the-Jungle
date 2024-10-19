using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroAnimaticManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene("MazeInstructionScreen");
        AudioManager.instance.StopAllSounds();
        // Play instruction music when loading instructions
        AudioManager.instance.Play("InterludeTheme");
    }
}
