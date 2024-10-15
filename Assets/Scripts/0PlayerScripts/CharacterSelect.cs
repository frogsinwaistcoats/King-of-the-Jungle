using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public static CharacterSelect instance;

    public List<Character> characters = new List<Character>();
    public List<GameObject> UIPrompts;
    public List<GameObject> characterSelections;
    public GameObject startButton;
    public int haveJoined;


    public void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }

        haveJoined = 0;
        ResetCharacterChoices();
    }

    public void ResetCharacterChoices()
    {
        foreach (Character character in characters)
        {
            character.isChosen = false;
        }
    }
}
