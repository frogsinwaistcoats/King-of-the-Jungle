using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts;
    public Image[] characterImages;
    public GameObject[] playerUI;

    GameManager gameManager;
    SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameManager = GameManager.instance;


        if (gameManager.players.Count <= 3)
        {
            playerUI[playerUI.Length - 1].SetActive(false);
            scoreTexts[scoreTexts.Length - 1].text = "";
            scoreTexts[scoreTexts.Length - 1] = null;
            characterImages[characterImages.Length - 1].enabled = false;
        }
        if (gameManager.players.Count == 2)
        {
            playerUI[playerUI.Length - 2].SetActive(false);
            scoreTexts[scoreTexts.Length - 2].text = "";
            scoreTexts[scoreTexts.Length - 2] = null;
            characterImages[characterImages.Length - 2].enabled = false;
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
            scoreTexts[0].text = "+ " + playerFirst.playerScore;
            characterImages[0].sprite = playerFirst.characterSprite;
        }

        if (scoreTexts[1] != null)
        {
            scoreTexts[1].text = "+ " + playerSecond.playerScore;
            characterImages[1].sprite = playerSecond.characterSprite;
        }

        if (scoreTexts[2] != null)
        {
            scoreTexts[2].text = "+ " + playerThird.playerScore;
            characterImages[2].sprite = playerThird.characterSprite;
        }


        if (scoreTexts[3] != null)
        {
            scoreTexts[3].text = "+ " + playerFourth.playerScore;
            characterImages[3].sprite = playerFourth.characterSprite;
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
            scoreTexts[0].text = playerFirst.totalScore.ToString();
            characterImages[0].sprite = playerFirst.characterSprite;
        }

        if (scoreTexts[1] != null)
        {
            scoreTexts[1].text = playerSecond.totalScore.ToString();
            characterImages[1].sprite = playerSecond.characterSprite;
        }

        if (scoreTexts[2] != null)
        {
            scoreTexts[2].text = playerThird.totalScore.ToString();
            characterImages[2].sprite = playerThird.characterSprite;
        }


        if (scoreTexts[3] != null)
        {
            scoreTexts[3].text = playerFourth.totalScore.ToString();
            characterImages[3].sprite = playerFourth.characterSprite;
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
