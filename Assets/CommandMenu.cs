using UnityEngine;
using TMPro;
using System.Collections;

public class CommandMenu : MonoBehaviour
{
    [SerializeField] private RectTransform cursor;
    [SerializeField] private TextMeshProUGUI[] commands;

    private int currentIndex = 0;

    public ShoppingSystem shop;

    public bool isShopList = false;
    public ShopCategory category;

    // メインメニューの場合のみ設定
    public int categoryOffset = 0;

    void Update()
    {
        if (commands == null || commands.Length == 0) return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            Decide();
        }
    }

    void MoveUp()
    {
        currentIndex = (currentIndex - 1 + commands.Length) % commands.Length;
        UpdateCursor();
    }

    void MoveDown()
    {
        currentIndex = (currentIndex + 1) % commands.Length;
        UpdateCursor();
    }

    void UpdateCursor()
    {
        if (commands == null || commands.Length == 0) return;

        RectTransform target = commands[currentIndex].GetComponent<RectTransform>();

        cursor.position = new Vector3(
            cursor.position.x,
            target.position.y,
            cursor.position.z
        );
    }

    void Decide()
    {
        Debug.Log("選択されたコマンド: " + commands[currentIndex].text);

        if (!isShopList)
        {
            shop.OpenCategory(currentIndex - categoryOffset);
        }
        else
        {
            shop.BuyItem(category, currentIndex);
        }
    }

    public void SetCommands(TextMeshProUGUI[] newCommands)
    {
        commands = newCommands;
        currentIndex = 0;
        StartCoroutine(DelayedCursorUpdate());
    }

    IEnumerator DelayedCursorUpdate()
    {
        yield return null;
        UpdateCursor();
    }
}