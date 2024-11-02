using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();

        _player.PlayerActionReader.OnPlayerMove += Rotate;
    }

    private void Rotate(Vector3 movementInput)
    {
        Quaternion targetRotation = Quaternion.LookRotation(movementInput);

        _player.PlayerRigidbody.rotation = Quaternion.Slerp(
            _player.PlayerRigidbody.rotation,
            targetRotation,
            _player.PlayerData.RotationSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        _player.PlayerActionReader.OnPlayerMove -= Rotate;
    }
}