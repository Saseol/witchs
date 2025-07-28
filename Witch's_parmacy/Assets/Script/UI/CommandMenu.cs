using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommandMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject menuPanel;        // 메뉴 전체 패널
    public TextMeshProUGUI[] options;   // 선택지 텍스트 (Attack, Skill 등)

    [Header("Settings")]
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;

    private int currentIndex = 0;
    private bool isOpen = false;
    private System.Action<string> onCommandSelected;

    private string[] commandNames = { "Attack", "Skill", "Item", "Defend" };

    void Start()
    {
        menuPanel.SetActive(false);
        UpdateSelection();
    }

    void Update()
    {
        if (!isOpen) return;

        // 위/아래 입력 처리
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = (currentIndex - 1 + options.Length) % options.Length;
            UpdateSelection();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = (currentIndex + 1) % options.Length;
            UpdateSelection();
        }

        // 선택 확정 (Enter 또는 Space)
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            SelectCommand();
        }

        // 취소 (예: ESC)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
        }
    }

    /// <summary>
    /// 메뉴 열기
    /// </summary>
    public void OpenMenu(System.Action<string> callback)
    {
        onCommandSelected = callback;
        menuPanel.SetActive(true);
        isOpen = true;
        currentIndex = 0;
        UpdateSelection();
    }

    /// <summary>
    /// 메뉴 닫기
    /// </summary>
    public void CloseMenu()
    {
        menuPanel.SetActive(false);
        isOpen = false;
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (i < 0) { i = 3;}
            if (i > 3) { i = 0; }

            options[i].color = (i == currentIndex) ? highlightColor : normalColor;
        }
    }

    private void SelectCommand()
    {
        string command = commandNames[currentIndex];
        Debug.Log("Command Selected: " + command);
        onCommandSelected?.Invoke(command);
        CloseMenu();
    }
}
