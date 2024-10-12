using UnityEngine;
using UnityEngine.UI;



public class ScoreManagerBumper : MonoBehaviour
{
    public static ScoreManagerBumper instance;
    int score = 0;
    private int currentScore;
    private int timer;
    public Text scoreText;// Use this for initialization

    private float startTime; // Time when the player starts
    private float elapsedTime; // Time that has passed since start
    private bool isTimerActive = true; // Flag to check if the timer is active


    void Start()
    {
        startTime = Time.time; // Record the start time
        score = 0; 
        {
            currentScore = 0;

        }
    }

    void Update()
    {
        // If the timer is active, calculate the elapsed time
        if (isTimerActive)
        {
            elapsedTime = Time.time - startTime;
            score = Mathf.FloorToInt(elapsedTime); // Convert elapsed time to a whole number score
        }

        // Update the UI to show the score
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }


        score = (int)Time.time;

    }


    public void StopTimer()
    {
        // Stop the timer when the player enters the collider
        isTimerActive = false;
    }
    private void Awake()
    {
        instance = this;
    }
  

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            currentScore = -10;
            HandleScore();
            StopTimer();
        }
    }
    private void HandleScore()
    {
        scoreText.text = "Score: " + currentScore;
    }

}



