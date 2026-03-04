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

        if (commands[currentIndex].text == "スキル購入")
        {
            FindObjectOfType<ShoppingSystem>().OpenSkillPanel();
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