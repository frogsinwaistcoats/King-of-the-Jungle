using UnityEngine;

[CreateAssetMenu(menuName = "Characters", fileName = "New Character")]
public class Character : ScriptableObject
{
    public Sprite characterSprite;
    public string characterName;
}
