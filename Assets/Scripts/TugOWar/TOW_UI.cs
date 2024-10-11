using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TOW_UI : MonoBehaviour
{
    public static TOW_UI instance;

    public List<char> playerButtonList = new List<char>();
    public int playerCount;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerCount = GameManager.instance.players.Count;
    }

    public (string, string) OpenReloadUI(UI_ReloadButton button1, UI_ReloadButton button2, int playerID, ControllerType controllerType)
    {
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        (string, string) randomKeys = RandomizeButton(controllerType, button1, button2);

        float randomX = 0;
        float randomY = 0;

        switch (playerID)
        {
            case 0:
                randomX = Random.Range(200, 760);
                randomY = Random.Range(740, 880);
                button1.GetComponent<Image>().color = new Color(0f, 76f / 255f, 1f, 1f);
                button2.GetComponent<Image>().color = new Color(0f, 76f / 255f, 1f, 1f);
                break;

            case 1:
                randomX = Random.Range(1160, 1720);
                randomY = Random.Range(740, 880);
                button1.GetComponent<Image>().color = new Color(215f / 255f, 94f / 255f, 244f / 255f, 1f);
                button2.GetComponent<Image>().color = new Color(215f / 255f, 94f / 255f, 244f / 255f, 1f);
                break;

            case 2:
                randomX = Random.Range(200, 760);
                randomY = Random.Range(200, 340);
                button1.GetComponent<Image>().color = new Color(1f, 231f / 255f, 38f / 255f, 1f);
                button2.GetComponent<Image>().color = new Color(1f, 231f / 255f, 38f / 255f, 1f);

                break;

            case 3:
                randomX = Random.Range(1160, 1720);
                randomY = Random.Range(200, 340);
                button1.GetComponent<Image>().color = new Color(144f / 255f, 1f, 92f / 255f, 1f);
                button2.GetComponent<Image>().color = new Color(144f / 255f, 1f, 92f / 255f, 1f);
                break;
        }

        button1.transform.position = new Vector2(randomX, randomY);
        button2.transform.position = new Vector2(randomX + 100, randomY);
        return randomKeys;
    }

    public (string, string) RandomizeButton(ControllerType controllerType, UI_ReloadButton button1, UI_ReloadButton button2)
    {
        string randomKey1 = string.Empty;
        string randomKey2 = string.Empty;

        randomKey1 = GetRandomKey(controllerType);
        do
        {
            randomKey2 = GetRandomKey(controllerType);
        }
        while (randomKey1 == randomKey2);

        SetButtonText(controllerType, randomKey1, button1);
        SetButtonText(controllerType, randomKey2, button2);

        return (randomKey1, randomKey2);
    }

    public string GetRandomKey(ControllerType controllerType)
    {
        string[] keys;
        string randomKey = string.Empty;

        switch (controllerType)
        {
            case ControllerType.Keyboard:
                keys = new string[] { "w", "a", "s", "d" };
                randomKey = keys[Random.Range(0, keys.Length)];
                break;

            case ControllerType.Xbox:
                keys = new string[] { "buttonEast", "buttonWest", "buttonNorth", "buttonSouth" };
                randomKey = keys[Random.Range(0, keys.Length)];
                break;
        }

        return randomKey;
    }

    public void SetButtonText (ControllerType controllerType, string randomKey, UI_ReloadButton button)
    {
        switch (controllerType)
        {
            case ControllerType.Keyboard:
                button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = randomKey;
                break;

            case ControllerType.Xbox:

                switch (randomKey)
                {
                    case "buttonEast":
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "b";
                        break;

                    case "buttonWest":
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "x";
                        break;

                    case "buttonNorth":
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "y";
                        break;

                    case "buttonSouth":
                        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "a";
                        break;
                }
                break;
        }
    }
}
