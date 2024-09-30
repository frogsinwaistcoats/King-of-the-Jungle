using System.Collections;
using UnityEngine;

public class EndingAnimaticManager : MonoBehaviour
{
    SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = SceneLoader.instance;
    }
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3f);
        sceneLoader.LoadMainMenu();
    }
}
