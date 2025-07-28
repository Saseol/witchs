using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Battle/UnitIcon Data")]
public class UnitDataIcon : ScriptableObject
{
    public string unitName;
    public Sprite icon;

    [Header("Stats")]
    public float Actspeed = 20f;
}