using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopCategory : MonoBehaviour
{
    public ShopCategoryType type;

    public GameObject panel;
    public Transform content;
    public GameObject rowPrefab;

    public List<ItemBase> items = new List<ItemBase>();
}

public enum ShopCategoryType
{
    Skill,
    Weapon,
    ConsumableItem,
    Armor,
    Magic
}
