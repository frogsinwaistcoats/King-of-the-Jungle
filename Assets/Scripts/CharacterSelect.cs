using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public static CharacterSelect instance;

    public List<Character> characters = new List<Character>();

    public void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }

}
