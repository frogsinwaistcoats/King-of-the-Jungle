using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ui_QTE_button : MonoBehaviour, IPointerDownHandler
{
    
    public void OnPointerDown(PointerEventData eventData)
    {
        QTE_placeholder.instance.AttemptToReload();
        
        gameObject.SetActive(false);

    }

   
}
