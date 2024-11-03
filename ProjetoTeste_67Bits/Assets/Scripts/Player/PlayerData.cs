using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    [Tooltip("How fast your charchter moves")]
    public float Speed = 300f;
    [Tooltip("How fast your charachter rotates to look foward to the way you are moving to")]
    public float RotationSpeed = 10f;

    [Header("Punch")]
    [Tooltip("How strong your punch is")]
    public float Strength = 10f;
    [Tooltip("How far can you punch")]
    public float Punch_Range = 2f;

    [Header("Pile")]
    [Tooltip("How many enemies you can collect to your pile!")]
    public int MaxAmountToPile = 10;
}
