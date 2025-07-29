using UnityEngine;
using UnityEngine.UI;

// 타임라인에서 아이콘의 위치와 행동 게이지를 관리하는 클래스
public class ActionTimelineIcon : MonoBehaviour
{
    public RectTransform timelineBar; // 타임라인 바의 RectTransform
    public RectTransform icon;        // 유닛 아이콘의 RectTransform
    public float atGauge = 0f;        // 현재 행동 게이지 (0 ~ 100)
    public float speed = 20f;         // 게이지가 차는 속도

    private float minX; // 아이콘이 움직일 수 있는 최소 X값
    private float maxX; // 아이콘이 움직일 수 있는 최대 X값

    void Start()
    {
        // 타임라인 바의 크기를 기준으로 아이콘 이동 범위 설정
        float halfWidth = timelineBar.rect.width / 2f;
        minX = -halfWidth;
        maxX = halfWidth;
    }

    void Update()
    {
        // 매 프레임마다 행동 게이지 증가
        atGauge += speed * Time.deltaTime;
        if (atGauge > 100f) atGauge = 100f;

        // 게이지에 따라 아이콘 위치 계산
        float ratio = atGauge / 100f;
        float posX = Mathf.Lerp(minX, maxX, ratio);

        // 아이콘의 실제 위치 갱신 (Y축은 바에서 80만큼 위)
        Vector3 newPos = new Vector3(posX, timelineBar.position.y + 80f, 0);
        icon.localPosition = newPos;

        // 게이지가 100에 도달하면 초기화
        if (atGauge >= 100f)
        {
            atGauge = 0f;
        }
    }
}
