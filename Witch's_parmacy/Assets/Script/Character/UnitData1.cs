using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Battle/UnitIcon Data")]
// 유닛 아이콘 정보와 행동 속도를 저장하는 데이터 클래스
public class UnitDataIcon : ScriptableObject
{
    public string unitName;      // 유닛 이름
    public Sprite icon;          // 유닛 아이콘 이미지

    [Header("능력치")]
    public float Actspeed = 20f; // 행동 게이지가 차는 속도
}