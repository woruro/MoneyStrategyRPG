using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Item")]
public class ItemBase : ScriptableObject
{
    public string itemName;
    public int price;
    public string description;
}
