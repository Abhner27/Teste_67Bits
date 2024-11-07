using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPunchBySelection : PunchAction
{
    public override event Hit OnHit;

    private Enemy _enemy;

    public void SelectEnemy(Enemy enemy)
    {
        _enemy = enemy;
        Punch();
    }

    protected override void Punch()
    {
        if (_enemy != null) //if it finds
        {
            //Calculate the hit force vector
            Vector3 direction = _enemy.transform.position - transform.position;
            direction.Normalize();
            Vector3 force = _player.PlayerData.Strength * direction;

            OnHit?.Invoke();

            // Call the die function for the enemy!
            _enemy.Die(force);

            //Reset the value
            _enemy = null;
        }
    }
}