using System.Threading;
using System.Threading.Tasks;
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

public class CooldownHandler
{
    private readonly float _cooldownTime;

    public delegate Task CooldownDelegate(float time,
        CancellationTokenSource cancellationTokenSource,
        Progress<float> progress);

    public event CooldownDelegate OnCooldownStart;

    public CooldownHandler(float cooldownTime)
    {
        _cooldownTime = cooldownTime;
    }

    public async Task StartCooldownAsync(CancellationTokenSource cancellationTokenSource)
    {
        Progress<float> progress = new Progress<float>();
        OnCooldownStart?.Invoke(_cooldownTime, cancellationTokenSource, progress);

        await CustomTimeManager.WaitForGameTime(_cooldownTime, cancellationTokenSource.Token, progress);
    }
}

public abstract class CooldownHandlerEffectBase<T> : MonoBehaviour where T : Component
{
    [SerializeField] 
    protected T _targetComponent; //The component of which the implementer requires

    protected float _maximumScale; //The scale multiplier base number
    protected Vector3 _initialScale;

    protected virtual void Awake()
    {
        if (_targetComponent == null)
            _targetComponent = GetComponent<T>();
    }

    public async Task StartCooldownEffect(
        float time,
        CancellationTokenSource cancellationTokenSource,
        Progress<float> progress)
    {
        OnEffectStart();

        progress.ProgressChanged += ProgressChanged;

        void ProgressChanged(object sender, float value)
        {
            if (cancellationTokenSource.IsCancellationRequested)
                return;

            ApplyProgressEffect(value);
        }

        try
        {
            await CustomTimeManager.WaitForGameTime(time, cancellationTokenSource.Token, progress);
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Cooldown canceled.");
        }

        progress.ProgressChanged -=  ProgressChanged;

        OnEffectEnd();
    }

    private void ApplyProgressEffect(float progressValue)
    {
        //progressValue will go from 0f to 1f.
        float scaledValue = progressValue * _maximumScale;

        //Do some more logic if needed!
        ApplyAdditionalEffect(scaledValue);
    }

    protected abstract void OnEffectStart(); //Called ate the start of the progress
    protected abstract void ApplyAdditionalEffect(float scaledValue); //Called during progress
    protected abstract void OnEffectEnd(); //Called ate the end of the progress
}
