using System.Threading;
using System;
using UnityEngine;

public class CollectEnemies : MonoBehaviour
{
    private const float COOLDOWN_TIME = 3f;

    private PlayerPile _playerPile;
    private bool _playerIsInside;

    public delegate void CooldownStart(
        float time,
        CancellationTokenSource cancellationTokenSource,
        Progress<float> progress);
    public event CooldownStart OnCooldownStart;

    private CancellationTokenSource _cancellationTokenSource;

    private void OnTriggerEnter(Collider collider)
    {
        if (_playerIsInside)
            return;

        _playerPile = collider.GetComponent<PlayerPile>();

        if (_playerPile.Pile.Count == 0)
            return;

        _playerIsInside = true;
        CollectOperation();
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!_playerIsInside)
            return;

        _playerIsInside = false;
        _cancellationTokenSource.Cancel();
    }

    private async void CollectOperation()
    {
        // Create a CancellationTokenSource to cancel the cooldown effect if necessary
        _cancellationTokenSource = new CancellationTokenSource();

        // Progress of the cooldown (from 0 to 1)
        Progress<float> progress = new Progress<float>();

        //Invoke CooldownEvent
        OnCooldownStart?.Invoke(COOLDOWN_TIME, _cancellationTokenSource, progress);

        //Wait for cooldown
        await CustomTimeManager.WaitForGameTime(COOLDOWN_TIME, _cancellationTokenSource.Token, progress);

        //If nt cancelled, clean the pile!
        if (!_cancellationTokenSource.IsCancellationRequested)
            _playerPile.CleanPile();
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}

public class ShopUITrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopUI;
    private void OnTriggerEnter(Collider collider)
    {
        _shopUI.SetActive(true);
    }
    private void OnTriggerExit(Collider collider)
    {
        _shopUI.SetActive(false);
    }
}