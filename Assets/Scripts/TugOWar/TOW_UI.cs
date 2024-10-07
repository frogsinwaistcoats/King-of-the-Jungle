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

        //for (int i = 0; i < playerCount; i++)
        //{
        //    UI_ReloadButton newButton = Instantiate(buttonPrefab);

        //    newButton.transform.SetParent(GameObject.Find("Canvas").transform, false);

        //    reloadButtons.Add(buttonPrefab);

        //    OpenReloadUI();
        //}
    }

    public string OpenReloadUI(UI_ReloadButton button, int playerID, ControllerType controllerType)
    {
        button.gameObject.SetActive(true);
        string randomKey = RandomizeButton(controllerType, button);

        float randomX = 0;
        float randomY = 0;

        switch (playerID)
        {
            case 0:
                randomX = Random.Range(200, 760);
                randomY = Random.Range(740, 880);
                break;

            case 1:
                randomX = Random.Range(1160, 1720);
                randomY = Random.Range(740, 880);
                button.GetComponent<Image>().color = new Color(167f / 255f, 241f / 255f, 144f / 255f, 1f);
                break;

            case 2:
                randomX = Random.Range(200, 760);
                randomY = Random.Range(200, 340);
                button.GetComponent<Image>().color = new Color(248f / 255f, 190f / 255f, 132f / 255f, 1f);

                break;

            case 3:
                randomX = Random.Range(1160, 1720);
                randomY = Random.Range(200, 340);
                button.GetComponent<Image>().color = new Color(91f / 255f, 231f / 255f, 255f / 255f, 1f);
                break;
        }

        button.transform.position = new Vector2(randomX, randomY);
        return randomKey;
    }

    public string RandomizeButton(ControllerType controllerType, UI_ReloadButton button)
    {
        string[] keys;
        string randomKey = string.Empty;

        switch (controllerType)
        {
            case ControllerType.Keyboard:
                keys = new string[] { "w", "a", "s", "d" };
                randomKey = keys[Random.Range(0, keys.Length)];
                button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = randomKey;
                break;

            case ControllerType.Xbox:
                keys = new string[] { "buttonEast", "buttonWest", "buttonNorth", "buttonSouth" };
                randomKey = keys[Random.Range(0, keys.Length)];

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

        return randomKey;
    }
}
