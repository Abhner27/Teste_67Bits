using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    private const string IS_RUNNING_BOOL = "IsRunning";
    private const string PUNCH_TRIGGER = "Punch";

    private PlayerActionReader _playerActionReader;
    private Animator _animator;

    private bool _isMoving = false;

    private void Start()
    {
        _playerActionReader = GetComponentInParent<Player>().PlayerActionReader;
        _animator = GetComponent<Animator>();

        _playerActionReader.OnPlayerMove += RunAnimation;
        _playerActionReader.OnPlayerStoppedMoving += StopRunAnimation;
        _playerActionReader.OnPlayerPunch += PunchAnimation;
    }

    private void RunAnimation(Vector3 movementInput)
    {
        if (_isMoving == true)
            return;

        _isMoving = true;

        _animator.SetBool(IS_RUNNING_BOOL, _isMoving);
    }

    private void StopRunAnimation()
    {
        if (_isMoving == false)
            return;

        _isMoving = false;

        _animator.SetBool(IS_RUNNING_BOOL, _isMoving);
    }

    private void PunchAnimation()
    {
        _animator.SetTrigger(PUNCH_TRIGGER);
    }

    private void OnDestroy()
    {
        _playerActionReader.OnPlayerMove -= RunAnimation;
        _playerActionReader.OnPlayerStoppedMoving -= StopRunAnimation;
        _playerActionReader.OnPlayerPunch -= PunchAnimation;
    }
}
