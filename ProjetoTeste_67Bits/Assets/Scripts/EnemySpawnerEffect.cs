using System.Threading;
using System.Threading.Tasks;
using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemySpawnerEffect : MonoBehaviour
{
    //At cooldown, it changes the ring sprite color!
    //And starts an animation going from (0, 0, 1) to (0.5, 0.5, 1)

    [SerializeField]
    private EnemySpawner _enemySpawner;

    private SpriteRenderer _spriteRenderer;

    //the Maximum value of the scale
    private float _maximum;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _enemySpawner.OnCooldownStart += Cooldown;
    }

    private async void Cooldown(float time, CancellationTokenSource cancellationTokenSource, Progress<float> progress)
    {
        //Get the initial scale
        Vector3 initialScale = transform.localScale;

        //Reset the localScale to start the cooldown
        transform.localScale = new Vector3(0, 0, 1);

        //Change the color
        _spriteRenderer.color = Color.black;

        // Progress will increase the localScale
        progress.ProgressChanged += ProgressChanged;

        //Gets the Maximum value (the biggest it will get at x and y)
        _maximum = initialScale.x;

        void ProgressChanged(object sender, float value) //value will increase from 0 to 1.
        {
            value *= _maximum;

            transform.localScale = new Vector3(value, value, 1);
        }

        // Try to wait... if canceled, Debug it!
        try
        {
            await CustomTimeManager.WaitForGameTime(time, cancellationTokenSource.Token, progress);
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Cooldown canceled.");
        }

        progress.ProgressChanged -= ProgressChanged;

        //Change back the color
        _spriteRenderer.color = Color.white;

        //Go back to the original scale!
        transform.localScale = initialScale;
    }

    private void OnDestroy()
    {
        _enemySpawner.OnCooldownStart -= Cooldown;
    }
}