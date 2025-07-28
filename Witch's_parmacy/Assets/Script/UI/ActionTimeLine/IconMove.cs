using UnityEngine;
using UnityEngine.UI;

public class ActionTimelineIcon : MonoBehaviour
{
    public RectTransform timelineBar; // 게이지 바 RectTransform
    public RectTransform icon;        // 아이콘 RectTransform
    public float atGauge = 0f;        // 현재 게이지 (0 ~ 100)
    public float speed = 20f;         // 초당 증가량

    private float minX;
    private float maxX;

    void Start()
    {
        // timelineBar의 가로 크기에 맞춰서 시작/끝 좌표 계산
        float halfWidth = timelineBar.rect.width / 2f;
        minX = -halfWidth;
        maxX = halfWidth;
    }

    void Update()
    {
        // 게이지 증가
        atGauge += speed * Time.deltaTime;
        if (atGauge > 100f) atGauge = 100f;

        // 비율 계산
        float ratio = atGauge / 100f;
        float posX = Mathf.Lerp(minX, maxX, ratio);

        // 아이콘 위치 업데이트
        Vector3 newPos = new Vector3(posX, timelineBar.position.y + 80f, 0);
        icon.localPosition = newPos;

        if (atGauge >= 100f) // atgauge 초기화 (임시)
        {
            atGauge = 0f;
        }
    }
}
