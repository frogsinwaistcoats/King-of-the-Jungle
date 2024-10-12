using UnityEngine;
using UnityEngine.UI;

public class PlayerTimer : MonoBehaviour
{
    public Text scoreText;   
    private float startTime; 
    private float elapsedTime; 
    private bool isTimerActive = true;  
    private int score;      

    void Start()
    {
        startTime = Time.time; 
        score = 0;
    }

    void Update()
    {
        
        if (isTimerActive)
        {
            elapsedTime = Time.time - startTime;
            score = Mathf.FloorToInt(elapsedTime); // Convert elapsed time to a whole number score
        }

        
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }


    public void StopTimer()
    {
        isTimerActive = false;
    }
}
