using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    [Header("매니저들")]
    public CommandMenu commandMenu;   // 커맨드 메뉴 관리

    [Header("타임라인")]
    public RectTransform timelineBar; // 타임라인 바 UI
    public List<TimelineUnit> units;  // 전투에 참여하는 유닛 리스트

    // 현재 행동할 차례인 유닛
    private TimelineUnit activeUnit = null;

    void Start()
    {
        // 모든 유닛의 타임라인 위치 초기화
        foreach (var unit in units)
        {
            unit.Init(timelineBar);
        }
    }

    void Update()
    {
        // 이미 행동 중인 유닛이 있으면 대기
        if (activeUnit != null) return;

        // 모든 유닛의 게이지를 업데이트
        foreach (var unit in units)
        {
            unit.UpdateGauge(Time.deltaTime);

            // 게이지가 100%가 되면 행동 차례
            if (unit.IsReady() && activeUnit == null)
            {
                activeUnit = unit;

                if (unit.isPlayer)
                {
                    // 플레이어 유닛이면 커맨드 메뉴 오픈
                    commandMenu.OpenMenu(OnCommandChosen);

                }
                else
                {
                    // 적 유닛이면 AI로 자동 행동
                    ExecuteAction(unit, "Attack");
                }
            }
        }
    }

    /// <summary>
    /// 커맨드 메뉴에서 플레이어가 행동을 선택했을 때 호출
    /// </summary>
    void OnCommandChosen(string command)
    {
        ExecuteAction(activeUnit, command);
    }

    /// <summary>
    /// 실제 유닛의 행동을 실행
    /// </summary>
    void ExecuteAction(TimelineUnit unit, string command)
    {
        Debug.Log($"{unit.unitName}가 {command}를 사용합니다!");

        // TODO: 실제 공격, 스킬, 아이템 등 행동 구현

        // 행동 후 게이지 초기화
        unit.ResetGauge();

        // 다음 턴을 위해 activeUnit 초기화
        activeUnit = null;
    }
}
