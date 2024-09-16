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
                randomX = Random.Range(300, 500);
                randomY = Random.Range(300, 700);
                break;

            case 1:
                randomX = Random.Range(900, 1100);
                randomY = Random.Range(300, 700);
                button.GetComponent<Image>().color = Color.red;
                break;

            case 2:
                randomX = Random.Range(500, 700);
                randomY = Random.Range(300, 700);
                button.GetComponent<Image>().color = Color.blue;

                break;

            case 3:
                randomX = Random.Range(1100, 1300);
                randomY = Random.Range(300, 700);
                button.GetComponent<Image>().color = Color.green;
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
