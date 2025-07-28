using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    [Header("Unit Info")]
    public string unitName = "Justin";
    public int maxHP = 100;
    public int currentHP;
    public int attackPower = 20;
    public int defense = 5;
    public float speed = 20f; // ������ ä��� �ӵ�

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

        // ������ ����
        if (atGauge < 100f)
        {
            atGauge += speed * Time.deltaTime;
            if (atGauge > 100f) atGauge = 100f;
            UpdateIconPosition();
        }
    }

    /// <summary>
    /// �������� �غ�Ǿ����� Ȯ��
    /// </summary>
    public bool IsReady()
    {
        return atGauge >= 100f && isAlive;
    }

    /// <summary>
    /// �ൿ ���� �� ������ �ʱ�ȭ
    /// </summary>
    public void ResetGauge()
    {
        atGauge = 0f;
        UpdateIconPosition();
    }

    /// <summary>
    /// ���� ó��
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
