using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    private Player _player;

    [SerializeField] 
    private LayerMask _enemyLayer;

    private void Start()
    {
        _player = GetComponent<Player>();

        _player.PlayerActionReader.OnPlayerPunch += Punch;
    }

    private void Punch()
    {
        RaycastHit hit;
        Vector3 punchOrigin = transform.position + Vector3.up;
        Vector3 punchDirection = transform.forward;

        if (Physics.Raycast(punchOrigin, punchDirection, out hit, _player.PlayerData.Punch_Range, _enemyLayer))
        {
            // Try to find the "Enemy" component in the hit go
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                //Calculate the hit force vector
                Vector3 direction = hit.collider.transform.position - punchOrigin;
                direction.Normalize();
                Vector3 force = _player.PlayerData.Strength * direction;

                // Call the die function for the enemy!
                enemy.Die(force);
            }
        }
    }

    private void OnDestroy()
    {
        _player.PlayerActionReader.OnPlayerPunch -= Punch;
    }
}

public class DestroyInimigos : MonoBehaviour
{

}