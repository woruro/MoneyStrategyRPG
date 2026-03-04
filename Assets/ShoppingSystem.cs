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

    public List<Skill> shopSkills = new List<Skill>();
    public List<Weapon> shopWeapons = new List<Weapon>();

    public GameObject skillTextPrefab;    // 作ったPrefab

    private List<TextMeshProUGUI> skillTexts = new List<TextMeshProUGUI>();

    public Transform skillContent;   // スキルパネル内の親オブジェクト

    private void Start()
    {
        CreateShopData();
        UpdateUI();
    }

    void CreateShopData()
    {
        // ===== スキル3つ =====
        shopSkills.Add(new Skill
        {
            itemName = "パワースラッシュ",
            power = 25,
            mpCost = 5,
            price = 300,
            description = "強力な物理攻撃"
        });

        shopSkills.Add(new Skill
        {
            itemName = "ヒール",
            power = 20,
            mpCost = 4,
            price = 250,
            description = "HPを回復する"
        });

        shopSkills.Add(new Skill
        {
            itemName = "ファイアブレード",
            power = 30,
            mpCost = 8,
            price = 500,
            description = "炎属性攻撃"
        });

        // ===== 武器3つ =====
        shopWeapons.Add(new Weapon
        {
            itemName = "鉄の剣",
            atkBonus = 5,
            price = 400,
            description = "攻撃力+5"
        });

        shopWeapons.Add(new Weapon
        {
            itemName = "鋼の剣",
            atkBonus = 10,
            price = 800,
            description = "攻撃力+10"
        });

        shopWeapons.Add(new Weapon
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

    public void OpenSkillPanel()
    {
        Debug.Log("スキルパネルを開こうとしています");
        OpenSubPanel(skillPanel);
        GenerateSkillList();
    }

    public void OpenItemPanel()
    {
        OpenSubPanel(itemPanel);
    }

    public void OpenWeaponPanel()
    {
        OpenSubPanel(weaponPanel);
    }

    public void OpenArmorPanel()
    {
        OpenSubPanel(armorPanel);
    }

    public void OpenMagicPanel()
    {
        OpenSubPanel(magicPanel);
    }

    void GenerateSkillList()
    {
        // 既存の子オブジェクト削除
        foreach (Transform child in skillContent)
        {
            Destroy(child.gameObject);
        }

        skillTexts.Clear();

        for (int i = 0; i < shopSkills.Count; i++)
        {
            GameObject obj = Instantiate(skillTextPrefab, skillContent);
            TextMeshProUGUI text = obj.GetComponent<TextMeshProUGUI>();

            text.text = shopSkills[i].itemName;

            skillTexts.Add(text);
        }

        // CommandMenuにテキスト配列を渡す
        CommandMenu menu = skillPanel.GetComponent<CommandMenu>();
        menu.SetCommands(skillTexts.ToArray());
    }

    public void BuySkill(int index)
    {
        Skill skill = shopSkills[index];

        // すでに持っているかチェック
        if (GameData.Instance.ownedSkills.Contains(skill))
        {
            Debug.Log("すでに購入済み");
            return;
        }

        // お金が足りるか
        if (GameData.Instance.money >= skill.price)
        {
            GameData.Instance.money -= skill.price;
            GameData.Instance.ownedSkills.Add(skill);

            Debug.Log(skill.itemName + " を購入しました！");
            UpdateUI();
        }
        else
        {
            Debug.Log("お金が足りません");
        }
    }

    public void BuyLevel()
    {
        int cost = 200;
        if (GameData.Instance.money >= cost)
        {
            GameData.Instance.money -= cost;
            GameData.Instance.level++;
            UpdateUI();
        }
    }

    public void BuyAttack()
    {
        int cost = 150;
        if (GameData.Instance.money >= cost)
        {
            GameData.Instance.money -= cost;
            GameData.Instance.atk += 5;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        moneyText.text = $"所持金：{GameData.Instance.money}G";
        statusText.text = $"Lv {GameData.Instance.level}　ATK {GameData.Instance.atk}";
    }
}