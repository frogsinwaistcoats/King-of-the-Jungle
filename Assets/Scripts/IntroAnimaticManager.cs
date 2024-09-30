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
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MazeInstructionScreen");
    }
}
