using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 플레이어가 행동을 선택할 수 있는 커맨드 메뉴 UI를 관리하는 클래스
public class CommandMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject menuPanel;        // 커맨드 메뉴 UI 패널
    public TextMeshProUGUI[] options;   // 행동 선택지 텍스트 (Attack, Skill 등)

    [Header("설정값")]
    public Color normalColor = Color.white;      // 기본 텍스트 색상
    public Color highlightColor = Color.yellow;  // 선택된 텍스트 색상

    private int currentIndex = 0; // 현재 선택된 인덱스
    private bool isOpen = false;  // 메뉴가 열려있는지 여부
    private System.Action<string> onCommandSelected; // 행동 선택 시 콜백

    private string[] commandNames = { "Attack", "Skill", "Item", "Defend" }; // 선택 가능한 행동 이름

    void Start()
    {
        // 시작 시 메뉴를 숨기고 선택지 초기화
        menuPanel.SetActive(false);
        UpdateSelection();
    }

    void Update()
    {
        if (!isOpen) return;

        // 위/아래 방향키로 선택 이동
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

        // 엔터 또는 스페이스로 선택 확정
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            SelectCommand();
        }

        // ESC로 메뉴 닫기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
        }
    }

    /// <summary>
    /// 커맨드 메뉴를 열고 콜백을 등록
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
    /// �޴� �ݱ�
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
