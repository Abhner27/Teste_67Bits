using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerRotation : MonoBehaviour
{
    private const float ROTATION_THRESHOLD = 0.1f;

    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();

        _player.PlayerActionReader.OnPlayerMove += Rotate;
    }

    private void Rotate(Vector3 movementInput)
    {
        //Get the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(movementInput);

        //If rotation is too close to target, stop rotating!
        if (Quaternion.Angle(_player.PlayerRigidbody.rotation, targetRotation) < ROTATION_THRESHOLD)
            return;

        //Rotate!
        _player.PlayerRigidbody.rotation = Quaternion.Slerp(
            _player.PlayerRigidbody.rotation,
            targetRotation,
            _player.PlayerData.RotationSpeed * Time.deltaTime);
    }

    public void LookAt(Vector3 point)
    {
        transform.rotation = Quaternion.LookRotation(point);
    }

    private void OnDestroy()
    {
        _player.PlayerActionReader.OnPlayerMove -= Rotate;
    }
}
