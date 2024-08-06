using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishManager : MonoBehaviour
{
    MultiplayerInputManager inputManager;
    public List<Collider> finishTriggers = new List<Collider>();

    private void Awake()
    {
        inputManager = MultiplayerInputManager.instance;
    }

    private void Update()
    {
        
    }

    //public int PlayersFinished
    //{
    //    //get { return ; }
    //}
}
