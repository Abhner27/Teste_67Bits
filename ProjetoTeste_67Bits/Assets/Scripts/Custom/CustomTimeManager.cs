using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using System;

public class CustomTimeManager
{
    private static float _currentTime = 0f;
    private static float _pausedTime = 0f;

    private static bool _canCountTime = true;

    public static void Intialize()
    {
        //Here you can set your pauses/resume functions
        //Example
        //GameManager.Instance.GameSpeedController.OnPause += OnPause;
        //GameManager.Instance.GameSpeedController.OnResume += OnResume;

        CountTime();
    }

    private static async Task CountTime()
    {
        while(_canCountTime)
        {
            _currentTime = Time.time - _pausedTime;
            await Task.Yield();
        }
    }

    private static async Task PausedCountTime()
    {
        while (!_canCountTime)
        {
            _pausedTime = Time.time - _currentTime;
            await Task.Yield();
        }
    }

    public static async Task WaitForGameTime(float tempo)
    {
        float tempoDeEspera = Time.time + tempo;

        while(Time.time < tempoDeEspera)
        {
            await Task.Yield();
        }
    }

    public static async Task WaitForGameTime(float tempo, CancellationToken token)
    {
        float tempoDeEspera = Time.time + tempo;

        while (Time.time < tempoDeEspera)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }

            await Task.Yield();
        }
    }

    public static async Task WaitForGameTime(float tempo, CancellationToken token, IProgress<float> progress)
    {
        float tempoDeEspera = Time.time + tempo;

        while (Time.time < tempoDeEspera)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }

            await Task.Yield();          
            progress.Report(1f - ((tempoDeEspera - Time.time) / tempo));
        }
    }

    private static void OnPause()
    {
        _canCountTime = false;
        PausedCountTime();
    }

    private static void OnResume()
    {
        _canCountTime = true;
        CountTime();
    }
}

public class ChangeableTimer
{
    private const float TIME_CHANGE_RATE = 0.1f;

    private float _maxTime = 0f;
    private float _time = 0f;

    public ChangeableTimer(float time)
    {
        _maxTime = _time = time;
    }

    public ChangeableTimer(float time, float maxTime)
    {
        _time = time;
        _maxTime = maxTime;
    }

    public async Task WaitForTimeToBeZero(CancellationToken token)
    {
        while (_time > 0f)
        {
            _time -= TIME_CHANGE_RATE;
            await CustomTimeManager.WaitForGameTime(TIME_CHANGE_RATE);

            if (token.IsCancellationRequested)
            {
                return;
            }
        }
    }

    public async Task WaitForTimeToBeZero(CancellationToken token, IProgress<float> progress)
    {
        while (_time > 0f)
        {
            progress.Report(_time / _maxTime);

            _time -= TIME_CHANGE_RATE;

            await CustomTimeManager.WaitForGameTime(TIME_CHANGE_RATE);

            if (token.IsCancellationRequested)
            {
                return;
            }
        }
    }

    public void AddAmountToTime(float addAmount)
    {
        _time += addAmount;

        if (_time > _maxTime)
            _time = _maxTime;
    }
}