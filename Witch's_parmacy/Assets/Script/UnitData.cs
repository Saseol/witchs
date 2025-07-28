using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Battle/Unit Data")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public Sprite icon;
    public GameObject prefab; // �������� ����� ������

    [Header("Stats")]
    public int maxHP = 100;
    public int attackPower = 20;
    public int defense = 5;
    public float speed = 20f;
}