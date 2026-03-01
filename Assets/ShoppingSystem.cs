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

    private void Start()
    {
        UpdateUI();
    }

    // ----------------------------
    // サブパネル共通処理
    // ----------------------------

    void OpenSubPanel(GameObject panel)
    {
        // すでに何か開いているなら閉じる
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
        OpenSubPanel(skillPanel);
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

    public void BuySkill()
    {
        int cost = 300;
        if (GameData.Instance.money >= cost && !GameData.Instance.hasSkill)
        {
            GameData.Instance.money -= cost;
            GameData.Instance.hasSkill = true;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        moneyText.text = $"所持金：{GameData.Instance.money}G";
        statusText.text = $"Lv {GameData.Instance.level}　ATK {GameData.Instance.atk}　SKILL: {(GameData.Instance.hasSkill ? "あり" : "なし")}";
    }
}