using UnityEngine;

// 플레이어 유닛의 전투 상태와 행동 게이지를 관리하는 클래스
public class PlayerUnit : MonoBehaviour
{
    [Header("유닛 정보")]
    public string unitName = "Justin"; // 유닛 이름
    public int maxHP = 100;             // 최대 체력
    public int currentHP;               // 현재 체력
    public int attackPower = 20;        // 공격력
    public int defense = 5;             // 방어력
    public float speed = 20f;           // 행동 게이지가 차는 속도

    [Header("전투 상태")]
    public bool isAlive = true;         // 생존 여부
    private float atGauge = 0f;         // 현재 행동 게이지 (0~100)


    void Start()
    {
        // 시작 시 체력 초기화 및 아이콘 위치 갱신
        currentHP = maxHP;
        UpdateIconPosition();
    }

    void Update()
    {
        if (!isAlive) return;

        // 행동 게이지가 100 미만일 때 증가 및 아이콘 위치 갱신
        if (atGauge < 100f)
        {
            atGauge += speed * Time.deltaTime;
            if (atGauge > 100f) atGauge = 100f;
            UpdateIconPosition();
        }
    }

    /// <summary>
    /// 행동 게이지가 100에 도달했고 살아있으면 행동 가능
    /// </summary>
    public bool IsReady()
    {
        return atGauge >= 100f && isAlive;
    }

    /// <summary>
    /// 행동 후 게이지를 0으로 초기화
    /// </summary>
    public void ResetGauge()
    {
        atGauge = 0f;
        UpdateIconPosition();
    }

    /// <summary>
    /// 데미지 처리: 방어력을 고려하여 체력 감소
    /// </summary>
    public void TakeDamage(int damage)
    {
        int finalDamage = Mathf.Max(1, damage - defense);
        currentHP -= finalDamage;
        Debug.Log($"{unitName} took {finalDamage} damage! HP: {currentHP}/{maxHP}");

        if (currentHP <= 0)
        {
            currentHP = 0;
            isAlive = false;
            Debug.Log($"{unitName} is defeated!");
        }
    }

    private void UpdateIconPosition()
    {
        float ratio = atGauge / 100f;
    }
}
