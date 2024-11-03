using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public float Speed = 300f;
    public float RotationSpeed = 10f;
    public float Strength = 10f;
    public float Punch_Range = 2f;
}
