using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionReader : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    //For Movement
    public delegate void PlayerMove(Vector3 movementVector);
    public event PlayerMove OnPlayerMove;

    //For Punching
    public delegate void PlayerPunch();
    public event PlayerPunch OnPlayerPunch;

    private void Awake()
    {
        //Initialize
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        //Introduce the punch read at the performed phase
        _playerInputActions.Player.Punch.performed += Punch;
    }

    //Movement input is checked every single Fixed Update!
    private void FixedUpdate()
    {
        Vector2 movementInput = _playerInputActions.Player.Movement.ReadValue<Vector2>();

        if (movementInput == Vector2.zero)
            return;

        OnPlayerMove?.Invoke(new Vector3(movementInput.x, 0, movementInput.y));
    }

    //Punch is called by a UI button or other InputActions in the Player Input actionMap
    public void Punch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPlayerPunch?.Invoke();
        }
    }
}
