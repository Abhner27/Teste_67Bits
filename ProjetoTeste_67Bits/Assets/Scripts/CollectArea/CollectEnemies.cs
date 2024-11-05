using System.Threading;
using System;
using UnityEngine;

[RequireComponent(typeof(CollectArea))]
public class CollectEnemies : MonoBehaviour
{
    private CollectArea _collectArea;
    private PlayerPile _playerPile; //Reference to clear the player pile

    private CooldownHandler _cooldownHandler;
    public CooldownHandler CooldownHandler { get => _cooldownHandler; private set { } }

    CancellationTokenSource _cancellationTokenSource;

    private void Awake()
    {
        _collectArea = GetComponent<CollectArea>();
        _playerPile = FindFirstObjectByType<PlayerPile>();

        _cooldownHandler = new CooldownHandler(3f);

        _collectArea.OnEnter += PlayerEnter;
        _collectArea.OnExit += PlayerExit;
    }

    private void PlayerEnter()
    {
        if (_playerPile.Pile.Count == 0)
            return;

        CollectOperation();
    }

    private void PlayerExit()
    {
        if (_cancellationTokenSource == null)
            return;

        try
        {
            _cancellationTokenSource.Cancel();
        }
        catch (ObjectDisposedException)
        {
            Debug.Log("O CancellationTokenSource foi disposed.");
        }
    }

    private async void CollectOperation()
    {
        using (_cancellationTokenSource = new CancellationTokenSource())
        {
            await _cooldownHandler.StartCooldownAsync(_cancellationTokenSource);

            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _playerPile.CleanPile();
            }

            _cancellationTokenSource.Dispose();
        }
    }

    private void OnDestroy()
    {
        _collectArea.OnEnter -= PlayerEnter;
        _collectArea.OnExit -= PlayerExit;
    }
}
