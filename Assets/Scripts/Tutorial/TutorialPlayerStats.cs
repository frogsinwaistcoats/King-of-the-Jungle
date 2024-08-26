using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerStats : MonoBehaviour
{
    public PlayerData playerData;

    [SerializeField] SpriteRenderer rend;

    public void UpdatePlayer(PlayerData data)
    {
        playerData = data;
        rend.sprite = CharacterSelect.instance.characters[playerData.characterIndex].characterSprite;
    }
}
