using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadSceneAsync("ManagerScene", LoadSceneMode.Additive);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("CharacterSelection");
            }
        }
    }
}
