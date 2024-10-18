using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerData playerData;

    public SpriteRenderer rend;

    public void UpdatePlayer(PlayerData data)
    {
        playerData = data;
        rend.sprite = CharacterSelect.instance.characters[playerData.characterIndex].characterSprite;
        //rend.sprite = playerData.characterSprite; //Put this to test if it would fix the ending scene sprites
    }
}
