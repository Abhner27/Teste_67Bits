using UnityEngine;

[RequireComponent(typeof(PunchAction))]
public class PlayerPunchEffects : MonoBehaviour
{
    private PunchAction _playerPunch;

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