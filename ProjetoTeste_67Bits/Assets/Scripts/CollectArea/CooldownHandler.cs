using System.Threading;
using System.Threading.Tasks;
using System;

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
