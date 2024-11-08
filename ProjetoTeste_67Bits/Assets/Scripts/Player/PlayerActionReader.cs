using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionReader : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    //For Movement
    public delegate void PlayerMove(Vector3 movementVector);
    public event PlayerMove OnPlayerMove;

    public delegate void PlayerStopped();
    public event PlayerStopped OnPlayerStoppedMoving;

    //For Punching
    public delegate void PlayerPunch();
    public event PlayerPunch OnPlayerPunch;

    //For Buying XP
    public delegate void PlayerBuy();
    public event PlayerBuy OnPlayerBuy;

    //For Interacting
    public delegate void PlayerInteract(Vector2 pos);
    public event PlayerInteract OnPlayerInteract;

    private void Awake()
    {
        //Initialize
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        //Introduce the punch read at the performed phase
        _playerInputActions.Player.Punch.performed += Punch;
        _playerInputActions.Player.Buy.performed += Buy;
    }

    //Movement input is checked every single Fixed Update!
    private void FixedUpdate()
    {
        //Movement
        Vector2 movementInput = _playerInputActions.Player.Movement.ReadValue<Vector2>();

        if (movementInput == Vector2.zero)
        {
            OnPlayerStoppedMoving?.Invoke();
            return;
        }

        OnPlayerMove?.Invoke(new Vector3(movementInput.x, 0, movementInput.y));
    }

    private void Update()
    {
        Interact();
    }

    //Punch is called by a UI button or other InputActions in the Player Input actionMap
    private void Punch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPlayerPunch?.Invoke();
        }
    }

    private void Buy(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPlayerBuy?.Invoke();
        }
    }

    private void Interact()
    {
        if ((Touchscreen.current != null && Touchscreen.current.press.wasPressedThisFrame) |
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame))
        {
            Vector2 pos = _playerInputActions.Player.Interact.ReadValue<Vector2>();

            OnPlayerInteract?.Invoke(pos);
        }
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Punch.performed -= Punch;
        _playerInputActions.Player.Buy.performed -= Buy;
    }
}
