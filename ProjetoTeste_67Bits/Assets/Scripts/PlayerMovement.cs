using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();

        _player.PlayerActionReader.OnPlayerMove += Move;
    }

    private void Move(Vector3 movementInput)
    {
        _player.PlayerRigidbody.velocity = _player.PlayerData.Speed * Time.deltaTime * movementInput;
    }

    private void OnDestroy()
    {
        _player.PlayerActionReader.OnPlayerMove -= Move;
    }
}
