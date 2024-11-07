using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInteraction : MonoBehaviour
{
    private const float INTERACT_DISTANCE_CONSTANT = 0.5f;

    private Player _player;

    [SerializeField]
    private LayerMask _interactableLayer = 6;

    [SerializeField]
    private Camera _camera;

    public delegate void PlayerInteract();
    public event PlayerInteract OnPlayerSuccessfullInteraction;

    private void Start()
    {
        _player = GetComponent<Player>();

        if (_camera == null)
            _camera = Camera.main;

        _player.PlayerActionReader.OnPlayerInteract += TryInteract;
    }

    private void TryInteract(Vector2 touchPosition)
    {
        RaycastHit hit;

        Ray ray = _camera.ScreenPointToRay(touchPosition);

        //See if it hits a collider!
        if (!Physics.Raycast(ray, out hit, _interactableLayer))
            return;

        //Check player distance to the click/touch position. (if it is bigger than player range, return)
        if (Vector3.Distance(transform.position, hit.transform.position) > (_player.PlayerData.Punch_Range + INTERACT_DISTANCE_CONSTANT))
            return;

        //Try get the interactable component!
        IInteractable interactable = hit.collider.GetComponent<IInteractable>();

        if (interactable != null) //if it finds
        {
            interactable.Interact(_player); //Interact!
            OnPlayerSuccessfullInteraction?.Invoke();
        }
    }

    private void OnDestroy()
    {
        _player.PlayerActionReader.OnPlayerInteract -= TryInteract;
    }
}
