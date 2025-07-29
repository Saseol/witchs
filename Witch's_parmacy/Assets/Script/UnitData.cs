using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Battle/Unit Data")]
// 유닛의 기본 정보와 전투 능력치를 저장하는 데이터 클래스
public class UnitData : ScriptableObject
{
    public string unitName;      // 유닛 이름
    public Sprite icon;          // 유닛 아이콘 이미지
    public GameObject prefab;    // 유닛 프리팹 오브젝트

    [Header("능력치")]
    public int maxHP = 100;         // 최대 체력
    public int attackPower = 20;    // 공격력
    public int defense = 5;         // 방어력
    public float speed = 20f;       // 행동 게이지가 차는 속도
}