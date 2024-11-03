using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemy : MonoBehaviour
{
    private const float DURATION_TIME = 30f;
    private const float COLLECT_ACTIVATION_TIME = 0.5f;

    private BoxCollider _boxCollider;

    private async void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.enabled = false;

        await CustomTimeManager.WaitForGameTime(COLLECT_ACTIVATION_TIME);

        _boxCollider.enabled = true;
        Destroy(gameObject, DURATION_TIME);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Coletado!");
            other.GetComponent<PlayerPile>().AddToPile();
            Destroy(gameObject);
        }
    }
}
