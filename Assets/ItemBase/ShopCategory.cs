using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopCategory
{
    public string categoryName;

    public GameObject panel;

    public GameObject rowPrefab;

    public Transform content;

    public List<ItemBase> items = new List<ItemBase>();
}
