using UnityEngine;

[RequireComponent(typeof(PlayerPunch))]
public class PlayerPunchEffects : MonoBehaviour
{
    private PlayerPunch _playerPunch;

    //Play some particles at the hit!
    [SerializeField]
    private ParticleSystem _hitParticles;

    private void Start()
    {
        _playerPunch = GetComponent<PlayerPunch>();

        _playerPunch.OnHit += PlayParticles;
    }
    
    private void PlayParticles()
    {
        _hitParticles.Play();
    }

    private void OnDestroy()
    {
        _playerPunch.OnHit -= PlayParticles;
    }

}