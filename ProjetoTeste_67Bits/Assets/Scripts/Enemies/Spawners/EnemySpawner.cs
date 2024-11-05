using System.Threading;
using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemySpawner : MonoBehaviour
{
    private Enemy _enemy;

    private CooldownHandler _cooldownHandler;
    public CooldownHandler CooldownHandler { get => _cooldownHandler; private set { } }

    private CancellationTokenSource _cancellationTokenSource;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();

        _cooldownHandler = new CooldownHandler(2f);

        _enemy.EnemyCooldownEvent += Cooldown;
    }

    private async void Cooldown()
    {
        using (_cancellationTokenSource = new CancellationTokenSource())
        {
            await _cooldownHandler.StartCooldownAsync(_cancellationTokenSource);

            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _enemy.GetActive();
            }
        }
    }

    private void OnDestroy()
    {
        _enemy.EnemyCooldownEvent -= Cooldown;
    }
}
