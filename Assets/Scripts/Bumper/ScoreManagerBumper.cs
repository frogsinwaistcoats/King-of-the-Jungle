using UnityEngine;
using UnityEngine.UI;



public class ScoreManagerBumper : MonoBehaviour
{
    public static ScoreManagerBumper instance;
    int score = 0;
    private int currentScore;
    private int timer;
    public Text scoreText;// Use this for initialization

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentScore = 0;

    }
    private void HandleScore()
    {
        scoreText.text = "Score: " + currentScore;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            currentScore++;
            HandleScore();
        }
    }


    void Update()
    {

        score = (int)Time.time; 

        if (timer > 5f)
        {

            score += 2;

            //We only need to update the text if the score changed.
            scoreText.text = score.ToString();

            //Reset the timer to 0.
            timer = 0;
        }
    }
}



