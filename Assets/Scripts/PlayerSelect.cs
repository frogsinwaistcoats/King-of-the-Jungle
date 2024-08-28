using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    public int playerID;

    [SerializeField] GameManager gameManager;
    [SerializeField] CharacterSelect characterSelect;

    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI joinText;
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI characterText;

    int currentCharacter = 0;

    private void Start()
    {
        SetCharacter(currentCharacter);
    }

    public void NextCharacter()
    {
        currentCharacter++;
        if (currentCharacter >= characterSelect.characters.Count)
        {
            currentCharacter = 0;
        }

        SetCharacter(currentCharacter);
    }

    public void PreviousCharacter()
    {
        currentCharacter--;
        if (currentCharacter <= -1)
        {
            currentCharacter = characterSelect.characters.Count - 1;
        }

        SetCharacter(currentCharacter);
    }

    public void SetCharacter(int id)
    {
        characterImage.sprite = characterSelect.characters[id].characterSprite;
        characterText.text = characterSelect.characters[id].characterName;
    }

    public void Confirm()
    {
        gameManager.AddPlayer(playerID, currentCharacter);
    }
}
