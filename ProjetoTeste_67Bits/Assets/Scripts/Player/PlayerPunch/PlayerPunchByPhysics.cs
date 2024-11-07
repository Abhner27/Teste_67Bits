using UnityEngine;

public class PlayerPunchByPhysics : PunchAction
{
    [SerializeField]
    private LayerMask _enemyLayer;

    public override event Hit OnHit;

    private void Awake()
    {
        _enemyLayer = LayerMask.GetMask(new string[1] { "Enemy" });
    }

    protected override void Punch()
    {
        RaycastHit hit;
        float radius = 0.5f;

        //Calculate the punch origin and final positions
        Vector3 punchOrigin = transform.position + Vector3.up;
        Vector3 punchDirection = transform.forward;
        Vector3 finalPunchPosition = punchOrigin - (punchDirection.normalized * radius);

        //Make a SphereCast to check for a hit!
        //It is a SphereCast instead of a Raycast to allow players to have some area of impact (leading to less "missed punches")
        if (Physics.SphereCast(finalPunchPosition, radius, punchDirection, out hit, _player.PlayerData.Punch_Range, _enemyLayer))
        {
            // Try to find the "Enemy" component in the hit gameObject
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if (enemy != null) //if it finds
            {
                //Calculate the hit force vector
                Vector3 direction = hit.collider.transform.position - punchOrigin;
                direction.Normalize();
                Vector3 force = _player.PlayerData.Strength * direction;

                OnHit?.Invoke();

                // Call the die function for the enemy!
                enemy.Die(force);
            }
        }
    }
}
