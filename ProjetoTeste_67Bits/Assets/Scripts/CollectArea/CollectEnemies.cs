using System.Threading;
using System;
using UnityEngine;

public class CollectEnemies : MonoBehaviour
{
    private const float COOLDOWN_TIME = 3f;

    private PlayerPile _playerPile;
    private bool _playerIsInside;

    [SerializeField]
    private GameObject _shopUI;

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
        _shopUI.SetActive(true);
        CollectOperation();
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!_playerIsInside)
            return;

        _playerIsInside = false;
        _shopUI.SetActive(false);
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
}
