using UnityEngine;

public class CollectArea : MonoBehaviour
{
    private bool _playerIsInside = true;

    public delegate void AreaInteractions();
    public event AreaInteractions OnEnter;
    public event AreaInteractions OnExit;

    private void OnTriggerEnter(Collider collider)
    {
        if (_playerIsInside)
            return;

        if (collider.GetComponent<Player>() != null)
        {
            _playerIsInside = true;
            OnEnter?.Invoke();
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (!_playerIsInside)
            return;

        if (collider.GetComponent<Player>() != null)
        {
            _playerIsInside = false;
            OnExit?.Invoke();
        }
    }
}
