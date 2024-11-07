using UnityEngine;

[RequireComponent(typeof(PlayerPile))]
public class PileWindWobbleAnimation : MonoBehaviour
{
    //Script to generate a wind wobble offset to the player pile!

    private PlayerPile _playerPile;

    [SerializeField]
    [Range(0.01f, 1f)]
    private float _windWobbleSpeed = 0.1f;

    [SerializeField]
    [Range(0.1f, 0.5f)]
    private float _windWobbleOscilation = 0.15f;

    private void Start()
    {
        _playerPile = GetComponent<PlayerPile>();
    }

    private void FixedUpdate()
    {
        if (_playerPile.IsPileActive) //If pile is active, apply the wind wobble effect!
            _playerPile.ApplyOffset(WindWobble());
    }

    private Vector3 WindWobble()
    {
        float pingPong = Mathf.PingPong(Time.time * _windWobbleSpeed, _windWobbleOscilation);

        return new Vector3(pingPong, 0f, 0f);
    }

    private void OnDisable()
    {
        _playerPile.ApplyOffset(Vector3.zero); //Reset the offset to zero!
    }
}