using UnityEngine;

public class DeadEnemy : MonoBehaviour
{
    //Lifetime of this gameobject. - Necessary so it don't flood the scene!
    private const float DURATION_TIME = 30f;

    //Time before player can start to collect this enemy to its pile!
    private const float COLLECT_ACTIVATION_TIME = 1f;

    private bool _canBeCollected = false;
    public bool CanBeCollected { get => _canBeCollected; private set { } }

    private async void Start()
    {
        //Start lifetime timer
        Destroy(gameObject, DURATION_TIME);

        //Wait for activating the collection trigger
        await CustomTimeManager.WaitForGameTime(COLLECT_ACTIVATION_TIME);

        //Allow collection!
        _canBeCollected = true;
    }
}
