using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerData playerData;

    [SerializeField] SpriteRenderer rend;

    //public Animator animator;

    public void UpdatePlayer(PlayerData data)
    {
        playerData = data;
        rend.sprite = CharacterSelect.instance.characters[playerData.characterIndex].characterSprite;
        //animator.SetBool(rend.sprite.name, true);
    }
}
