using UnityEngine;
using TMPro;

public class CommandMenu : MonoBehaviour
{
    [SerializeField] private RectTransform cursor;
    [SerializeField] private TextMeshProUGUI[] commands;
    [SerializeField] private UnityEngine.Events.UnityEvent[] actions;

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
        RectTransform target = commands[currentIndex].rectTransform;

        cursor.anchoredPosition = new Vector2(
            cursor.anchoredPosition.x,
            target.anchoredPosition.y
        );
    }

    void Decide()
    {
        Debug.Log("選択されたコマンド: " + commands[currentIndex].text);
        actions[currentIndex].Invoke();
    }
}