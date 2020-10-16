using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters")]
public class NewCharacter : ScriptableObject
{
    public string characterName;

    public Sprite characterImage;
    
    public int health;
    public int strength;
    public int dexterity;
    public int stamina;
    public int artifice;
}
