using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTE_placeholder : MonoBehaviour
{
    [SerializeField] private BoxCollider window;
    public static QTE_placeholder instance;
    [Header("QTE Details")]
    [SerializeField] private int QTEsteps;
    [SerializeField] private TOWPlayerInput _input;
    [SerializeField] private Ui_QTE_button[] QTE_Buttons;


    

    public void Awake()
    {
        instance = this; 
    }
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        OpenQTE();
    //    }
    //}

        void Start()
    {

        QTE_Buttons = GetComponentsInChildren <Ui_QTE_button>(true); // tbh the vid showed a diff script  i just merged it
    }
    public void AttemptToReload()
    {
        if (QTEsteps > 0)
            QTEsteps--;
        if (QTEsteps <=0)
        {
           // _input.OnPull();
        }
    }
    public void OpenQTE()
    {
        foreach (Ui_QTE_button button in QTE_Buttons)
        {
            button.gameObject.SetActive(true);
            float randomX = Random.Range(window.bounds.min.x, window.bounds.max.x);
            float randomY = Random.Range(window.bounds.min.y, window.bounds.max.y);
            button.transform.position = new Vector2(randomX, randomY);
        }


        QTEsteps = QTE_Buttons.Length;
    }
}
