using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    public int playerID;

    [SerializeField] GameManager gameManager;
    [SerializeField] CharacterSelect characterSelect;
    [SerializeField] string playerName;

    [Header ("UI Elements")]
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI characterText;
    [SerializeField] TMP_InputField playerInputField;

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
        if (currentCharacter <= - 1)
        {
            currentCharacter = characterSelect.characters.Count - 1;
        }

        SetCharacter(currentCharacter);
    }

    public void SetCharacter(int ID)
    {
        characterImage.sprite = characterSelect.characters[ID].characterSprite;
        characterText.text = characterSelect.characters[ID].characterName;
    }

    public void Confirm()
    {
        playerName = playerInputField.text;
        gameManager.SetPlayer(playerID, currentCharacter, playerName);
    }
}
