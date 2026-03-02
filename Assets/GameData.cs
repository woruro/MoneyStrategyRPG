using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Progress;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    // ===== 基本情報 =====
    public int money = 1000;
    public int level = 1;

    // ===== HP / MP =====
    public int maxHp = 100;
    public int hp = 100;

    public int maxMp = 30;
    public int mp = 30;

    // ===== 物理系 =====
    public int atk = 10;      // 物理攻撃力
    public int def = 5;       // 物理防御力

    // ===== 魔法系 =====
    public int matk = 8;      // 魔法攻撃力
    public int mdef = 4;      // 魔法防御力

    // ===== 補助能力 =====
    public int spd = 7;       // 素早さ（行動順）
    public int dex = 6;       // 器用さ（クリティカル・命中）
    public int luk = 5;       // 運（ドロップ・状態異常）

    // ===== 所持データ =====
    public List<Skill> ownedSkills = new List<Skill>();
    public List<Magic> ownedMagics = new List<Magic>();
    public List<Weapon> ownedWeapons = new List<Weapon>();
    public List<Armor> ownedArmors = new List<Armor>();
    public List<ConsumableItem> ownedItems = new List<ConsumableItem>();

    private void Awake()
    {
        // シングルトン化
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}