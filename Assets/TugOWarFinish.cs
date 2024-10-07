using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TugOWarFinish : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;

    private void Start()
    {
        gameOverText.enabled = false;
    }

    public void GameOver()
    {
        gameOverText.enabled = true;
    }
}
