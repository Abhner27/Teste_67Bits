using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _deadEnemyPrefab;

    public void Die(Vector3 force)
    {
        //Instantiate a ragdoll
        GameObject _ragdollEnemy = Instantiate(_deadEnemyPrefab, transform.position, Quaternion.identity);

        //Apply a force to the ragdoll!
        Rigidbody rb = _ragdollEnemy.GetComponentInChildren<Rigidbody>();
        rb.AddForce(force, ForceMode.Impulse);

        //This enemy spawner goes to cooldown!


        //Eliminate this enemy (set the active state to false) - GC isn't called
        gameObject.SetActive(false);
    }
}

public class EnemySpawner : MonoBehaviour
{
    private void Start()
    {
        
    }

    private async void Cooldown()
    {

    }

    private void OnDestroy()
    {
        
    }
}