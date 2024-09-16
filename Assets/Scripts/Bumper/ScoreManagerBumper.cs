using UnityEngine;
using UnityEngine.UI;



public class ScoreManagerBumper : MonoBehaviour
{
    public static ScoreManagerBumper instance;
    int score = 0;
    private void Awake()
    {
        instance = this;
    }
    public void AddPoint()
    {
            score += 1;
    }
    public void LosePoint()
    {
        score = 0;
    }


}