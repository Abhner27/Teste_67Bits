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

    [SerializeField]
    private Transform _graphicsTransform;
    public Transform GraphicsTransform { get => _graphicsTransform; private set { } }

    private void OnValidate()
    {
        if (_playerActionReader == null)
            _playerActionReader = gameObject.AddComponent<PlayerActionReader>();

        if (_playerRigidbody == null)
        {
            _playerRigidbody = gameObject.AddComponent<Rigidbody>();
            _playerRigidbody.drag = 10f;
            _playerRigidbody.angularDrag = 10f;
            _playerRigidbody.useGravity = false;
        }
    }
}
