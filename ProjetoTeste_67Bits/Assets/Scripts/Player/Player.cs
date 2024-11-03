using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerData _playerData;
    public PlayerData PlayerData { get => _playerData; private set { } }

    [SerializeField]
    private PlayerActionReader _playerActionReader;
    public PlayerActionReader PlayerActionReader { get => _playerActionReader; private set { } }

    [SerializeField]
    private Rigidbody _playerRigidbody;
    public Rigidbody PlayerRigidbody { get => _playerRigidbody; private set { } }
}
