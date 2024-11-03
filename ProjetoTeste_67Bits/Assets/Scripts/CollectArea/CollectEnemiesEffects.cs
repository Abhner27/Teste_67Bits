using System.Threading;
using System.Threading.Tasks;
using System;
using UnityEngine;

[RequireComponent(typeof(CollectEnemies))]
public class CollectEnemiesEffects : MonoBehaviour 
{
    private CollectEnemies _collectEnemies;

    [SerializeField]
    private GameObject _fillerSprite;

    //the Maximum value of the scale
    private float _maximum;

    private void Start()
    {
        _collectEnemies = GetComponent<CollectEnemies>();

        _collectEnemies.OnCooldownStart += Cooldown;
    }

    private async void Cooldown(float time, CancellationTokenSource cancellationTokenSource, Progress<float> progress)
    {
        //Get the initial scale
        Vector3 initialScale = _fillerSprite.transform.localScale;

        //Reset the localScale to start the cooldown
        _fillerSprite.transform.localScale = new Vector3(0, 0, 1);

        //Activate the fill
        _fillerSprite.SetActive(true);

        // Progress will increase the localScale
        progress.ProgressChanged += ProgressChanged;

        //Gets the Maximum value (the biggest it will get at x and y)
        _maximum = initialScale.x;

        void ProgressChanged(object sender, float value) //value will increase from 0 to 1.
        {
            value *= _maximum;

            _fillerSprite.transform.localScale = new Vector3(value, value, 1);
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

        //Go back to the original scale!
        _fillerSprite.transform.localScale = initialScale;

        //Set active state to false
        _fillerSprite.SetActive(false);
    }

    private void OnDestroy()
    {
        _collectEnemies.OnCooldownStart -= Cooldown;
    }
}