using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "RPG/ConsumableItem")]
public class ConsumableItem : ItemBase
{
    public int hpHeal;
    public int mpHeal;
}
