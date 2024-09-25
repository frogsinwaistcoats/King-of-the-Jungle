using UnityEngine;

[CreateAssetMenu(menuName = "Characters", fileName = "New Characters")]
public class Character : ScriptableObject
{
    public Sprite characterSprite;
    public string characterName;
    public bool isChosen = false;
}
