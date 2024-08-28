using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ReloadButton : MonoBehaviour , IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        MillieUI.instance.reloadSteps--;

        if (MillieUI.instance.reloadSteps <= 0)
        {

        }

        gameObject.SetActive(false);
    }
}
