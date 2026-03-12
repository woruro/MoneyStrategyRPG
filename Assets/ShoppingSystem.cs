using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingSystem : MonoBehaviour
{
    // UI表示
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI statusText;

    // メイン
    public GameObject mainPanel;

    // サブパネル
    public GameObject skillPanel;
    public GameObject itemPanel;
    public GameObject weaponPanel;
    public GameObject armorPanel;
    public GameObject magicPanel;

    // 今開いているサブパネル
    private GameObject currentSubPanel;

    public List<ShopCategory> categories = new List<ShopCategory>();

    private void Start()
    {
        CreateShopData();
        UpdateUI();
    }

    void CreateShopData()
    {
        ShopCategory skillCategory = categories[0];
        ShopCategory weaponCategory = categories[1];

        // ===== スキル =====

        skillCategory.items.Add(new Skill
        {
            itemName = "パワースラッシュ",
            power = 25,
            mpCost = 5,
            price = 300,
            description = "強力な物理攻撃"
        });

        skillCategory.items.Add(new Skill
        {
            itemName = "ヒール",
            power = 20,
            mpCost = 4,
            price = 250,
            description = "HPを回復する"
        });

        skillCategory.items.Add(new Skill
        {
            itemName = "ファイアブレード",
            power = 30,
            mpCost = 8,
            price = 500,
            description = "炎属性攻撃"
        });

        // ===== 武器 =====

        weaponCategory.items.Add(new Weapon
        {
            itemName = "鉄の剣",
            atkBonus = 5,
            price = 400,
            description = "攻撃力+5"
        });

        weaponCategory.items.Add(new Weapon
        {
            itemName = "鋼の剣",
            atkBonus = 10,
            price = 800,
            description = "攻撃力+10"
        });

        weaponCategory.items.Add(new Weapon
        {
            itemName = "伝説の剣",
            atkBonus = 20,
            price = 2000,
            description = "攻撃力+20"
        });
    }

    // ----------------------------
    // サブパネル共通処理
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
        // ESCキー or テンキー0
        if (Input.GetKeyDown(KeyCode.Escape) ||
            Input.GetKeyDown(KeyCode.Keypad0))
        {
            // サブパネルが開いているときだけ閉じる
            if (currentSubPanel != null)
            {
                CloseSubPanel();
            }
        }
    }

    // ----------------------------
    // 各パネルを開く関数
    // ----------------------------

    public void OpenCategory(ShopCategory category)
    {
        Debug.Log("Category panel: " + category.panel);

        OpenSubPanel(category.panel);

        GenerateList(category);
    }

    void GenerateList(ShopCategory category)
    {
        foreach (Transform child in category.content)
        {
            Destroy(child.gameObject);
        }

        List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();

        for (int i = 0; i < category.items.Count; i++)
        {
            GameObject obj = Instantiate(category.rowPrefab, category.content);

            TextMeshProUGUI nameText =
                obj.transform.Find("NameText").GetComponent<TextMeshProUGUI>();

            TextMeshProUGUI priceText =
                obj.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();

            nameText.text = category.items[i].itemName;
            priceText.text = category.items[i].price + "G";

            texts.Add(nameText);
        }

        CommandMenu menu = category.panel.GetComponent<CommandMenu>();
        menu.SetCommands(texts.ToArray());
    }

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
        statusText.text = $"Lv {GameData.Instance.level}　ATK {GameData.Instance.atk}";
    }
}