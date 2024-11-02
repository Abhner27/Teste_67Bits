using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerDataReset : MonoBehaviour
{
    //To reset the values of the current used PlayerData at the start!

    //References
    private Player _player;
    private PlayerData _playerData;

    //Values
    private float _initialSpeed;
    private float _initialRotationSpeed;
    private float _initialStrength;

    private void Start()
    {
        //Get the components
        _player = GetComponent<Player>();
        _playerData = _player.PlayerData;

        //Store the values
        _initialSpeed = _playerData.Speed;
        _initialRotationSpeed = _playerData.RotationSpeed;
        _initialStrength = _playerData.Strength;
    }

    private void OnDestroy()
    {
        //At the end, reset the values
        _playerData.Speed = _initialSpeed;
        _playerData.Speed = _initialRotationSpeed;
        _playerData.Strength = _initialStrength;
    }
}
