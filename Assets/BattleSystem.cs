using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    public TextMeshProUGUI playerHpText;
    public TextMeshProUGUI bossHpText;
    public TextMeshProUGUI messageText;

    int playerHp = 200;
    int bossHp = 500;
    int bossAtk = 15;   // ボスの攻撃力

    private void Start()
    {
        UpdateUI();
    }

    public void Attack()
    {
        // プレイヤー攻撃（ATKはGameManagerの値を使用）
        bossHp -= GameData.Instance.atk;

        if (bossHp <= 0)
        {
            bossHp = 0;
            UpdateUI();
            messageText.text = " 勝利！ボスを倒した！";
            return;
        }

        // ボス反撃
        playerHp -= bossAtk;

        if (playerHp <= 0)
        {
            playerHp = 0;
            UpdateUI();
            messageText.text = " 敗北…やられてしまった…";
            return;
        }

        UpdateUI();
        messageText.text = $" プレイヤーの攻撃！ {GameData.Instance.atk} ダメージ\n" +
                           $" ボスの反撃！ {bossAtk} ダメージ";
    }

    void UpdateUI()
    {
        playerHpText.text = $"HP：{playerHp}";
        bossHpText.text = $"BOSS HP：{bossHp}";
    }
}