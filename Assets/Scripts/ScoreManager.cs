using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts;

    GameManager gameManager;
    SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
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

        if (SceneManager.GetActiveScene().name == "Scores")
        {
            DisplayScores();
        }
        else if (SceneManager.GetActiveScene().name == "TotalScores")
        {
            DisplayTotalScores();
        }
        
    }

    public void DisplayScores()
    {
        PlayerData playerFirst = gameManager.players.OrderByDescending(p => p.playerScore).FirstOrDefault();
        PlayerData playerSecond = gameManager.players.OrderByDescending(p => p.playerScore).Skip(1).FirstOrDefault();
        PlayerData playerThird = gameManager.players.OrderByDescending(p => p.playerScore).Skip(2).FirstOrDefault();
        PlayerData playerFourth = gameManager.players.OrderByDescending(p => p.playerScore).Skip(3).FirstOrDefault();

        if (scoreTexts[0] != null)
        {
            string displayName = playerFirst.characterName;
            scoreTexts[0].text = "1st - " + displayName + "   +" + playerFirst.playerScore;
        }

        if (scoreTexts[1] != null)
        {
            string displayName = playerSecond.characterName;
            scoreTexts[1].text = "2nd - " + displayName + "   +" + playerSecond.playerScore;
        }

        if (scoreTexts[2] != null)
        {
            string displayName = playerThird.characterName;
            scoreTexts[2].text = "3rd - " + displayName + "   +" + playerThird.playerScore;
        }


        if (scoreTexts[3] != null)
        {
            string displayName = playerFourth.characterName;
            scoreTexts[3].text = "4th - " + displayName + "   +" + playerFourth.playerScore;
        }

    }

    public void DisplayTotalScores()
    {
        PlayerData playerFirst = gameManager.players.OrderByDescending(p => p.totalScore).FirstOrDefault();
        PlayerData playerSecond = gameManager.players.OrderByDescending(p => p.totalScore).Skip(1).FirstOrDefault();
        PlayerData playerThird = gameManager.players.OrderByDescending(p => p.totalScore).Skip(2).FirstOrDefault();
        PlayerData playerFourth = gameManager.players.OrderByDescending(p => p.totalScore).Skip(3).FirstOrDefault();

        if (scoreTexts[0] != null)
        {
            string displayName = playerFirst.characterName;
            scoreTexts[0].text = "1st - " + displayName + "    " + playerFirst.totalScore;
        }

        if (scoreTexts[1] != null)
        {
            string displayName = playerSecond.characterName;
            scoreTexts[1].text = "2nd - " + displayName + "    " + playerSecond.totalScore;
        }

        if (scoreTexts[2] != null)
        {
            string displayName = playerThird.characterName;
            scoreTexts[2].text = "3rd - " + displayName + "    " + playerThird.totalScore;
        }


        if (scoreTexts[3] != null)
        {
            string displayName = playerFourth.characterName;
            scoreTexts[3].text = "4th - " + displayName + "    " + playerFourth.totalScore;
        }

    }

    public void ResetScore()
    {
        foreach (var player in gameManager.players)
        {
            player.playerScore = 0;
        }
    }
}
