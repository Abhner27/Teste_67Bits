using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject _deadEnemyPrefab;

    public delegate void EnemyCooldown();
    public event EnemyCooldown EnemyCooldownEvent;

    public void Die(Vector3 force)
    {
        //Instantiate a ragdoll
        GameObject _ragdollEnemy = Instantiate(_deadEnemyPrefab, transform.position, Quaternion.identity);

        //Apply a force to the ragdoll!
        Rigidbody rb = _ragdollEnemy.GetComponentInChildren<Rigidbody>();
        rb.AddForce(force, ForceMode.Impulse);

        //This enemy spawner goes to cooldown!
        EnemyCooldownEvent?.Invoke();

        //Eliminate this enemy (set the active state to false) - GC isn't called
        gameObject.SetActive(false);
    }

    public void GetActive() => gameObject.SetActive(true);

    public void Interact(Player player)
    {
        PlayerPunchBySelection playerPunch = player.GetComponent<PlayerPunchBySelection>();

        if (playerPunch == null)
            return;

        playerPunch.SelectEnemy(this);
    }
}
