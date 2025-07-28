using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommandMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject menuPanel;        // �޴� ��ü �г�
    public TextMeshProUGUI[] options;   // ������ �ؽ�Ʈ (Attack, Skill ��)

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

        // ��/�Ʒ� �Է� ó��
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

        // ���� Ȯ�� (Enter �Ǵ� Space)
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            SelectCommand();
        }

        // ��� (��: ESC)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
        }
    }

    /// <summary>
    /// �޴� ����
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
