using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private const float CHECK_INTERVAL = 0.1f; // Interval between checks
    private const float COLLECT_DISTANCE = 2f; // Distance of the collect area

    private void Start()
    {
        CheckForDeadEnemies();
    }

    private async void CheckForDeadEnemies()
    {
        while (true)
        {
            //Use OverlapSphere to detect dead enemies nearby
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, COLLECT_DISTANCE);

            foreach (var hitCollider in hitColliders)
            {
                DeadEnemy deadEnemy = hitCollider.GetComponent<DeadEnemy>();
                if (deadEnemy != null && deadEnemy.CanBeCollected) //Check to see if it is collectable
                {
                    // Try to collect
                    if (GetComponent<PlayerPile>().AddToPile())
                    {
                        Destroy(deadEnemy.gameObject); // Destroy dead enemy
                    }
                }
            }

            //Wait to check again!
            await CustomTimeManager.WaitForGameTime(CHECK_INTERVAL);
        }
    }
}
