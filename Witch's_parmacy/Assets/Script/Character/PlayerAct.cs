using UnityEngine;

public class PlayerAct : MonoBehaviour
{
    public float moveSpeed = 10f;
    public int maxHP = 100;
    public int currentHP;
    public int attackPower = 20;
    
    public GameObject enemy; // 적 캐릭터를 참조하기 위한 변수
    private Vector3 targetPosition;
    private bool isMoving = false;

    // 넉백 관련 변수
    private bool knockbackPending = false;
    private float knockbackTimer = 0f;

    void Start()
    {
        currentHP = maxHP;
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isMoving) // isMoving 중에는 Act 게이지가 차오르지 않음
        {
            MoveToTarget();
            IdleMove();
        }

        if (Input.GetKeyDown(KeyCode.A)) // 적에게 이동
        {
            MovePosition(enemy, 0.1f);
            // 넉백 예약: 가까워졌을 때
            if (enemy != null && Vector3.Distance(transform.position, enemy.transform.position) < 1.5f)
            {
                knockbackPending = true;
                knockbackTimer = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.S)) // 적에게 멀어지기
        {
            MovePosition(enemy, 7f); 
        }

        // 넉백 타이머 처리
        if (knockbackPending)
        {
            knockbackTimer += Time.deltaTime;
            if (knockbackTimer >= 0.5f)
            {
                KnockbackEnemy();
                knockbackPending = false;
            }
        }
    }

    public void IdleMove(float idleRange = 0.5f, float idleSpeed = 1f)
    {
        if (!isMoving)
        {
            float offsetX = Mathf.Sin(Time.time * idleSpeed) * idleRange;
            float offsetY = Mathf.Cos(Time.time * idleSpeed) * idleRange * 0.5f;
            transform.position = targetPosition + new Vector3(offsetX, offsetY, 0);
        }
    }

    public void SetMoveTarget(Vector3 position)
    {
        targetPosition = position;
        isMoving = true;
    }


public void MovePosition(GameObject enemy, float distance)
    {
        if (enemy == null) return;

        Vector3 direction = (transform.position - enemy.transform.position).normalized;
        Vector3 awayPosition = enemy.transform.position + direction * distance; // enemy로부터 멀어지는 위치 계산
        awayPosition = new Vector3(
            Mathf.Clamp(awayPosition.x, -10f, 10f),
            Mathf.Clamp(awayPosition.y, -10f, 10f),
            Mathf.Clamp(awayPosition.z, -10f, 10f)
        ); // AABB 범위 제한
        SetMoveTarget(awayPosition);
       
    }
    
     // 넉백은 Update에서 타이머로 처리
    // 0.5초 뒤 넉백 처리 함수
    private void KnockbackEnemy()
    {
        if (enemy == null) return;
        Vector3 direction = (enemy.transform.position - transform.position).normalized;
        float knockbackDistance = 1f;
        Vector3 knockbackPosition = enemy.transform.position + direction * knockbackDistance;

        // AABB 범위 제한
        knockbackPosition = new Vector3(
            Mathf.Clamp(knockbackPosition.x, -10f, 10f),
            Mathf.Clamp(knockbackPosition.y, -10f, 10f),
            Mathf.Clamp(knockbackPosition.z, -10f, 10f)
        );

        enemy.transform.position = knockbackPosition;
    }
    

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            isMoving = false;
        }
    }

    public void Attack(PlayerAct target)
    {
        if (target != null)
        {
            target.TakeDamage(attackPower);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    void Die()
    {
        // TODO: Add death animation or logic
        gameObject.SetActive(false);
    }
}