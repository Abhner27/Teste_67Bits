using UnityEngine;

[RequireComponent(typeof(Player))]
public abstract class PunchAction : MonoBehaviour
{
    protected Player _player;

    [SerializeField]
    protected LayerMask _enemyLayer;

    public delegate void Hit();
    public abstract event Hit OnHit;

    private void Start()
    {
        _player = GetComponent<Player>();

        _player.PlayerActionReader.OnPlayerPunch += Punch;
    }
    protected abstract void Punch();

    private void OnDestroy()
    {
        _player.PlayerActionReader.OnPlayerPunch -= Punch;
    }
}
