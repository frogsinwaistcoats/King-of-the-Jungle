using UnityEngine;
using UnityEngine.UI;



public class TOW_ScoreManager : MonoBehaviour
{
    public static TOW_ScoreManager instance;
    int score = 0;
    private void Awake()
    {
        instance = this;
    }
    public void AddPoint()
    {
            score += 1;
    }
   

}