using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.instance;

        if (gameManager.players.Count <= 3)
        {
            scoreTexts[scoreTexts.Length - 1].text = "";
            scoreTexts[scoreTexts.Length - 1] = null;
        }
        if (gameManager.players.Count == 2)
        {
            scoreTexts[scoreTexts.Length - 2].text = "";
            scoreTexts[scoreTexts.Length - 2] = null;
        }

        DisplayScores();
    }

    public void DisplayScores()
    {
        PlayerData playerFirst = gameManager.players.OrderByDescending(p => p.playerScore).FirstOrDefault();
        PlayerData playerSecond = gameManager.players.OrderByDescending(p => p.playerScore).Skip(1).FirstOrDefault();
        PlayerData playerThird = gameManager.players.OrderByDescending(p => p.playerScore).Skip(2).FirstOrDefault();
        PlayerData playerFourth = gameManager.players.OrderByDescending(p => p.playerScore).Skip(3).FirstOrDefault();

        if (scoreTexts[0] != null)
        {
            int displayID = playerFirst.playerID + 1;
            scoreTexts[0].text = "1st - Player " + displayID;
        }

        if (scoreTexts[1] != null)
        {
            int displayID = playerSecond.playerID + 1;
            scoreTexts[1].text = "2nd - Player " + displayID;
        }

        if (scoreTexts[2] != null)
        {
            int displayID = playerThird.playerID + 1;
            scoreTexts[2].text = "3rd - Player " + displayID;
        }


        if (scoreTexts[3] != null)
        {
            int displayID = playerFourth.playerID + 1;
            scoreTexts[3].text = "4th - Player " + displayID;
        }

    }
}
