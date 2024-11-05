using System.Threading;
using System.Threading.Tasks;
using System;
using UnityEngine;

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