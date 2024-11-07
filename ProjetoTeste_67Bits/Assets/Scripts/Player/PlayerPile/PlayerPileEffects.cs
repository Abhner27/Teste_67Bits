using UnityEngine;

[RequireComponent(typeof(PlayerPile))]
public class PlayerPileEffects : MonoBehaviour
{
    private PlayerPile _playerPile;

    [SerializeField]
    private ParticleSystem _fullParticles;
    [SerializeField]
    private ParticleSystem _clearParticles;

    private void Start()
    {
        _playerPile = GetComponent<PlayerPile>();

        _playerPile.OnFull += Full;
        _playerPile.OnClear += Clear;
    }

    private void Full()
    {
        _fullParticles.Play();
    }

    private void Clear()
    {
        _clearParticles.Play();
    }

    private void OnDestroy()
    {
        _playerPile.OnFull -= Full;
        _playerPile.OnClear -= Clear;
    }
}