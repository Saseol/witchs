using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TimelineUnit
{
    public string unitName;
    public RectTransform icon;  // Ÿ�Ӷ��� ������
    public float speed = 20f;   // ������ ä��� �ӵ�
    public bool isPlayer = true;

    private float atGauge = 0f;
    private float minX, maxX;

    public void Init(RectTransform timelineBar)
    {
        float halfWidth = timelineBar.rect.width / 2f;
        minX = -halfWidth;
        maxX = halfWidth;
        atGauge = 0f;
        UpdateIconPosition();
    }

    public void UpdateGauge(float deltaTime)
    {
        atGauge += speed * deltaTime;
        if (atGauge > 100f) atGauge = 100f;
        UpdateIconPosition();
    }

    public bool IsReady()
    {
        return atGauge >= 100f;
    }

    public void ResetGauge()
    {
        atGauge = 0f;
        UpdateIconPosition();
    }

    private void UpdateIconPosition()
    {
        float ratio = atGauge / 100f;
        float posX = Mathf.Lerp(minX, maxX, ratio);
        icon.localPosition = new Vector3(posX, 0, 0);
    }
}
