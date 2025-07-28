using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    [Header("Managers")]
    public CommandMenu commandMenu;   // 명령 메뉴

    [Header("Timeline")]
    public RectTransform timelineBar;
    public List<TimelineUnit> units;  // 전투 유닛 리스트

    private TimelineUnit activeUnit = null;

    void Start()
    {
        // 전투 시작 시 모든 유닛 초기화
        foreach (var unit in units)
        {
            unit.Init(timelineBar);
        }
    }

    void Update()
    {
        if (activeUnit != null) return; // 이미 명령 대기 중이면 게이지 멈춤

        foreach (var unit in units)
        {
            unit.UpdateGauge(Time.deltaTime);

            // 게이지 100% 도달 시
            if (unit.IsReady() && activeUnit == null)
            {
                activeUnit = unit;

                if (unit.isPlayer)
                {
                    // 플레이어 유닛이면 CommandMenu 오픈
                    commandMenu.OpenMenu(OnCommandChosen);

                }
                else
                {
                    // 적이면 AI 행동 실행
                    ExecuteAction(unit, "Attack");
                }
            }
        }



    }

    /// <summary>
    /// CommandMenu에서 플레이어가 행동을 고른 경우 호출
    /// </summary>
    void OnCommandChosen(string command)
    {
        ExecuteAction(activeUnit, command);

    }

    /// <summary>
    /// 실제 행동 실행
    /// </summary>
    void ExecuteAction(TimelineUnit unit, string command)
    {
        Debug.Log($"{unit.unitName} uses {command}!");

        // TODO: 실제 공격, 스킬, 아이템 사용 로직 작성


        // 행동 완료 후 게이지 초기화
        unit.ResetGauge();

        // 턴 종료
        activeUnit = null;

    }
}
