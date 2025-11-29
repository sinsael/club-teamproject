using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Scriptable Objects/StatData")]
public class StatSO : ScriptableObject
{
    public float maxHealth = 100;
    public float healthRegen;
}
