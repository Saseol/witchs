using UnityEngine;
using UnityEngine.UI;

public class ActionTimelineIcon : MonoBehaviour
{
    public RectTransform timelineBar; // ������ �� RectTransform
    public RectTransform icon;        // ������ RectTransform
    public float atGauge = 0f;        // ���� ������ (0 ~ 100)
    public float speed = 20f;         // �ʴ� ������

    private float minX;
    private float maxX;

    void Start()
    {
        // timelineBar�� ���� ũ�⿡ ���缭 ����/�� ��ǥ ���
        float halfWidth = timelineBar.rect.width / 2f;
        minX = -halfWidth;
        maxX = halfWidth;
    }

    void Update()
    {
        // ������ ����
        atGauge += speed * Time.deltaTime;
        if (atGauge > 100f) atGauge = 100f;

        // ���� ���
        float ratio = atGauge / 100f;
        float posX = Mathf.Lerp(minX, maxX, ratio);

        // ������ ��ġ ������Ʈ
        Vector3 newPos = new Vector3(posX, timelineBar.position.y + 80f, 0);
        icon.localPosition = newPos;

        if (atGauge >= 100f) // atgauge �ʱ�ȭ (�ӽ�)
        {
            atGauge = 0f;
        }
    }
}
