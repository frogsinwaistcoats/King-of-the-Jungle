using UnityEngine;
using UnityEngine.UI;

public class PlayerTimer : MonoBehaviour
{
    public Text scoreText;   
    public float survivalTime = 0f; 
    private bool isSurviving = true; 
    private int score; 

    void Start()
    {
        
        score = 0;
    }

    void Update()
    {
        
        if (isSurviving)
        {
           
            survivalTime += Time.deltaTime;

            
            score = Mathf.FloorToInt(survivalTime); 

            // Update the UI score text
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score.ToString();
            }
        }
    }

   
    public void StopSurvival()
    {
        isSurviving = false;
        Debug.Log("Player stopped surviving. Final Score: " + score);
    }
}
