using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPunchTouchScreen : PunchAction
{
    private const float CHECK_DISTANCE = 0.5f;

    [SerializeField]
    private Camera _camera;

    public override event Hit OnHit;

    private void Awake()
    {
        if (_camera == null)
            _camera = Camera.main;
    }

    protected override void Punch()
    {
        //if the game is not in touchScreen, return
        if (Touchscreen.current == null)
            return;

        //getClick position
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        //Make it to world point 
        Vector3 worldPosition = _camera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, _camera.nearClipPlane));

        //check player distance to the click.position. (if it is bigger than player range, return)
        if (Vector3.Distance(transform.position, worldPosition) > _player.PlayerData.Punch_Range)
            return;

        //Can find a hit target now!
        //Use OverlapSphere to detect enemies around click area
        Collider[] hitColliders = Physics.OverlapSphere(worldPosition, CHECK_DISTANCE, _enemyLayer);

        if (hitColliders.Length == 0)
            return;

        Enemy enemy = hitColliders[0].GetComponent<Enemy>();

        if (enemy != null) //if it finds
        {
            //If u find an enemy, the player should rotate to it and Punch it!
            GetComponent<PlayerRotation>()?.LookAt(enemy.transform.position);

            //Calculate the hit force vector
            Vector3 direction = enemy.transform.position - transform.position;
            direction.Normalize();
            Vector3 force = _player.PlayerData.Strength * direction;

            OnHit?.Invoke();

            // Call the die function for the enemy!
            enemy.Die(force);
        }
    }
}