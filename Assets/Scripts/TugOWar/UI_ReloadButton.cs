using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UI_ReloadButton : MonoBehaviour
{
    public TOWPlayerInput playerInput;
    public TextMeshProUGUI buttonText;

    public int buttonID;

    //public void OnButtonPress()
    //{
    //    TOW_UI.instance.reloadSteps--;

    //    if (TOW_UI.instance.reloadSteps <= 0)
    //    {
    //        playerInput.OnPullButton();
    //    }

    //    gameObject.SetActive(false);
    //}
}
