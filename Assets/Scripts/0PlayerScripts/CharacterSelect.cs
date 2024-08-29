using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public static CharacterSelect instance;

    public List<Character> characters = new List<Character>();
    public List<GameObject> UIPrompts;
    public List<GameObject> characterSelections;


    public void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }
}
