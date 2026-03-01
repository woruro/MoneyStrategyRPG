using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public int money = 1000;
    public int level = 1;
    public int atk = 10;
    public bool hasSkill = false;

    private void Awake()
    {
        // ƒVƒ“ƒOƒ‹ƒgƒ“‰»
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