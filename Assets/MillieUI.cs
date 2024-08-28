using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillieUI : MonoBehaviour
{
    public static MillieUI instance;

    [Header("Reload info")]
    public int reloadSteps;
    public UI_ReloadButton[] reloadButtons;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        reloadButtons = GetComponentsInChildren<UI_ReloadButton>(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OpenReloadUI();
        }
    }

    public void OpenReloadUI()
    {
        foreach (UI_ReloadButton button in reloadButtons)
        {
            button.gameObject.SetActive(true);

            float randomX = Random.Range(400, 1600);
            float randomY = Random.Range(300, 900);

            button.transform.position = new Vector2(randomX, randomY);
        }

        reloadSteps = reloadButtons.Length;
    }
}
