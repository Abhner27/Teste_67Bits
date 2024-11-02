using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public float Speed = 1f;
    public float RotationSpeed = 1f;
    public float Strength = 10f;
}
