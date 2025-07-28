using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Battle/Unit Data")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public Sprite icon;
    public GameObject prefab; // 전투에서 사용할 프리팹

    [Header("Stats")]
    public int maxHP = 100;
    public int attackPower = 20;
    public int defense = 5;
    public float speed = 20f;
}