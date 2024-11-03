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
    private float _initialPunchRange;

    private void Start()
    {
        //Get the components
        _player = GetComponent<Player>();
        _playerData = _player.PlayerData;

        //Store the values
        _initialSpeed = _playerData.Speed;
        _initialRotationSpeed = _playerData.RotationSpeed;
        _initialStrength = _playerData.Strength;
        _initialPunchRange = _playerData.Punch_Range;
    }

    private void OnDestroy()
    {
        //At the end, reset the values
        _playerData.Speed = _initialSpeed;
        _playerData.RotationSpeed = _initialRotationSpeed;
        _playerData.Strength = _initialStrength;
        _playerData.Punch_Range = _initialPunchRange;
    }
}
