using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
// 타임라인에서 유닛의 행동 게이지와 아이콘 위치를 관리하는 클래스
public class TimelineUnit
{
    public string unitName; // 유닛 이름
    public RectTransform icon;  // 타임라인에 표시되는 아이콘
    public float speed = 20f;   // 행동 게이지가 차는 속도
    public bool isPlayer = true; // 플레이어 유닛 여부

    private float atGauge = 0f; // 현재 행동 게이지 (0~100)
    private float minX, maxX;   // 아이콘이 움직일 수 있는 X축 범위

    // 타임라인 바 기준으로 아이콘 위치와 게이지 초기화
    public void Init(RectTransform timelineBar)
    {
        float halfWidth = timelineBar.rect.width / 2f;
        minX = -halfWidth;
        maxX = halfWidth;
        atGauge = 0f;
        UpdateIconPosition();
    }

    // 매 프레임마다 행동 게이지를 증가시키고 아이콘 위치 갱신
    public void UpdateGauge(float deltaTime)
    {
        atGauge += speed * deltaTime;
        if (atGauge > 100f) atGauge = 100f;
        UpdateIconPosition();
    }

    // 행동 게이지가 100에 도달하면 행동 준비 완료
    public bool IsReady()
    {
        return atGauge >= 100f;
    }

    // 행동 후 게이지를 0으로 초기화
    public void ResetGauge()
    {
        atGauge = 0f;
        UpdateIconPosition();
    }

    // 현재 게이지에 따라 아이콘의 X 위치를 갱신
    private void UpdateIconPosition()
    {
        float ratio = atGauge / 100f;
        float posX = Mathf.Lerp(minX, maxX, ratio);
        icon.localPosition = new Vector3(posX, 0, 0);
    }
}
