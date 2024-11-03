using System.Threading;
using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemySpawner : MonoBehaviour
{
    private const float COOLDOWN_TIME = 10f;

    private Enemy _enemy;

    public delegate void CooldownStart(
        float time, 
        CancellationTokenSource cancellationTokenSource, 
        Progress<float> progress);

    public event CooldownStart OnCooldownStart;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();

        _enemy.EnemyCooldownEvent += Cooldown;
    }

    private async void Cooldown()
    {
        // Create a CancellationTokenSource to cancel the cooldown effect if necessary
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        // Progress of the cooldown (from 0 to 1)
        Progress<float> progress = new Progress<float>();

        //Invoke CooldownEvent
        OnCooldownStart?.Invoke(COOLDOWN_TIME, cancellationTokenSource, progress);

        //Wait for cooldown
        await CustomTimeManager.WaitForGameTime(COOLDOWN_TIME, cancellationTokenSource.Token, progress);

        //Set enemy back to active!
        _enemy.GetActive();
    }

    private void OnDestroy()
    {
        _enemy.EnemyCooldownEvent -= Cooldown;
    }
}
