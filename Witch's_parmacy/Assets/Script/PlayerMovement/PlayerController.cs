using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    [Header("Unit Info")]
    public string unitName = "Justin";
    public int maxHP = 100;
    public int currentHP;
    public int attackPower = 20;
    public int defense = 5;
    public float speed = 20f; // 게이지 채우는 속도

    [Header("Battle State")]
    public bool isAlive = true;
    private float atGauge = 0f;


    void Start()
    {
        currentHP = maxHP;
        UpdateIconPosition();
    }

    void Update()
    {
        if (!isAlive) return;

        // 게이지 증가
        if (atGauge < 100f)
        {
            atGauge += speed * Time.deltaTime;
            if (atGauge > 100f) atGauge = 100f;
            UpdateIconPosition();
        }
    }

    /// <summary>
    /// 게이지가 준비되었는지 확인
    /// </summary>
    public bool IsReady()
    {
        return atGauge >= 100f && isAlive;
    }

    /// <summary>
    /// 행동 실행 후 게이지 초기화
    /// </summary>
    public void ResetGauge()
    {
        atGauge = 0f;
        UpdateIconPosition();
    }

    /// <summary>
    /// 피해 처리
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
