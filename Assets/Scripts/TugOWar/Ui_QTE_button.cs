using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ui_QTE_button : MonoBehaviour, IPointerDownHandler
{
    PullOnButtonPress pullOnButtonPress;

    private void Awake()
    {
        pullOnButtonPress = PullOnButtonPress.instance;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        if (QTE_Buttons <= 2)
        {
            pullOnButtonPress.PullTowardsTarget();
        }

    }
    void Update () 
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            OpenQTE();
        }
            
    }


    [Header("QTE Details")]
    public int QTEsteps;
    public PullOnButtonPress Pull;
    public Ui_QTE_button QTE_Buttons;

    public object instance { get; private set; }

    public void OpenQTE()
    {
        //QTE_Buttons = GetComponentInChildren;// tbh the vid showed a diff script  i just merged it
        QTE_Buttons. gameObject.SetActive(true);
        float randomX = Random.Range(400, 1600);
        float randomY = Random.Range(300, 900);
        QTE_Buttons.transform.position = new Vector2(randomX, randomY);

        QTEsteps = QTE_Buttons.Length;
    }
    


}
