using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class CommandMenu : MonoBehaviour
{
    [SerializeField] private RectTransform cursor;
    [SerializeField] private TextMeshProUGUI[] commands;

    private int currentIndex = 0;

    void Start()
    {
        UpdateCursor();
    }

    void Update()
    {
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

        ShoppingSystem shop = FindObjectOfType<ShoppingSystem>();

        if (commands[currentIndex].text == "スキル購入")
        {
            shop.OpenSkillPanel();
        }

        if (commands[currentIndex].text == "武器購入")
        {
            shop.OpenWeaponPanel();
        }

        // 今このCommandMenuがどのパネルに付いているかで分岐
        if (gameObject == shop.skillPanel)
        {
            shop.BuySkill(currentIndex);
        }
        if (gameObject == shop.weaponPanel)
        {
            shop.BuyWeapon(currentIndex);
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
        yield return null; // 1�t���[���҂�
        UpdateCursor();
    }
}