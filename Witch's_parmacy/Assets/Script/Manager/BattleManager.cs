using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    [Header("Managers")]
    public CommandMenu commandMenu;   // ��� �޴�

    [Header("Timeline")]
    public RectTransform timelineBar;
    public List<TimelineUnit> units;  // ���� ���� ����Ʈ

    private TimelineUnit activeUnit = null;

    void Start()
    {
        // ���� ���� �� ��� ���� �ʱ�ȭ
        foreach (var unit in units)
        {
            unit.Init(timelineBar);
        }
    }

    void Update()
    {
        if (activeUnit != null) return; // �̹� ��� ��� ���̸� ������ ����

        foreach (var unit in units)
        {
            unit.UpdateGauge(Time.deltaTime);

            // ������ 100% ���� ��
            if (unit.IsReady() && activeUnit == null)
            {
                activeUnit = unit;

                if (unit.isPlayer)
                {
                    // �÷��̾� �����̸� CommandMenu ����
                    commandMenu.OpenMenu(OnCommandChosen);

                }
                else
                {
                    // ���̸� AI �ൿ ����
                    ExecuteAction(unit, "Attack");
                }
            }
        }



    }

    /// <summary>
    /// CommandMenu���� �÷��̾ �ൿ�� �� ��� ȣ��
    /// </summary>
    void OnCommandChosen(string command)
    {
        ExecuteAction(activeUnit, command);

    }

    /// <summary>
    /// ���� �ൿ ����
    /// </summary>
    void ExecuteAction(TimelineUnit unit, string command)
    {
        Debug.Log($"{unit.unitName} uses {command}!");

        // TODO: ���� ����, ��ų, ������ ��� ���� �ۼ�


        // �ൿ �Ϸ� �� ������ �ʱ�ȭ
        unit.ResetGauge();

        // �� ����
        activeUnit = null;

    }
}
