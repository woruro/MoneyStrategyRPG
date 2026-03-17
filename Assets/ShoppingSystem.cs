using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShoppingSystem : MonoBehaviour
{
    // UI表示
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI statusText;

    // メイン
    public GameObject mainPanel;

    // 今開いているサブパネル
    private GameObject currentSubPanel;

    public List<ShopCategory> categories = new List<ShopCategory>();

    ShopCategory GetCategory(ShopCategoryType type)
    {
        foreach (var c in categories)
        {
            if (c.type == type)
                return c;
        }

        return null;
    }

    private void Start()
    {
        CreateShopData();
        UpdateUI();
    }

    void CreateShopData()
    {
        ShopCategory skillCategory = GetCategory(ShopCategoryType.Skill);
        ShopCategory weaponCategory = GetCategory(ShopCategoryType.Weapon);
    }

    void AddItem(ShopCategoryType type, ItemBase item)
    {
        ShopCategory category = GetCategory(type);

        if (category != null)
            category.items.Add(item);
    }

    // ----------------------------
    // メニューから呼ばれる
    // ----------------------------

    public void OpenCategory(int categoryIndex)
    {
        if (categoryIndex < 0 || categoryIndex >= categories.Count)
        {
            Debug.Log("カテゴリ未実装");
            return;
        }

        ShopCategory category = categories[categoryIndex];

        OpenSubPanel(category.panel);
        GenerateList(category);
    }

    // ----------------------------
    // パネル管理
    // ----------------------------

    void OpenSubPanel(GameObject panel)
    {
        if (currentSubPanel != null)
        {
            currentSubPanel.SetActive(false);
        }

        mainPanel.SetActive(false);
        panel.SetActive(true);

        currentSubPanel = panel;
    }

    public void CloseSubPanel()
    {
        if (currentSubPanel != null)
        {
            currentSubPanel.SetActive(false);
            currentSubPanel = null;
        }

        mainPanel.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) ||
            Input.GetKeyDown(KeyCode.Keypad0))
        {
            if (currentSubPanel != null)
            {
                CloseSubPanel();
            }
        }
    }

    // ----------------------------
    // 商品生成
    // ----------------------------

    void GenerateList(ShopCategory category)
    {
        foreach (Transform child in category.content)
        {
            Destroy(child.gameObject);
        }

        List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();

        for (int i = 0; i < category.items.Count; i++)
        {
            ItemBase item = category.items[i];

            GameObject obj = Instantiate(category.rowPrefab, category.content);

            TextMeshProUGUI nameText =
                obj.transform.Find("NameText").GetComponent<TextMeshProUGUI>();

            TextMeshProUGUI priceText =
                obj.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();

            nameText.text = item.itemName;
            priceText.text = item.price + "G";

            texts.Add(nameText);
        }

        CommandMenu menu = category.panel.GetComponent<CommandMenu>();

        menu.category = category;
        menu.isShopList = true;

        menu.SetCommands(texts.ToArray());
    }

    // ----------------------------
    // 購入処理
    // ----------------------------

    public void BuyItem(ShopCategory category, int index)
    {
        ItemBase item = category.items[index];

        if (GameData.Instance.money >= item.price)
        {
            GameData.Instance.money -= item.price;

            Debug.Log(item.itemName + " を購入");

            UpdateUI();
        }
    }

    void UpdateUI()
    {
        moneyText.text = $"所持金：{GameData.Instance.money}G";
        statusText.text = $"Lv {GameData.Instance.level} ATK {GameData.Instance.atk}";
    }
}