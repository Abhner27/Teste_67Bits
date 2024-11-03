using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerPile : MonoBehaviour
{
    private List<Transform> _pile = new List<Transform>();
    private Player _player;

    [SerializeField]
    private GameObject _enemyPilePrefab;
    [SerializeField]
    private Transform _pileParentTransform;

    [SerializeField]
    private float _speedRate = 0.1f;
    [SerializeField]
    private Vector3 _offset = Vector3.up;

    void Start()
    {
        _player = GetComponent<Player>();
    }

    public void AddToPile()
    {
        if(_pile.Count == _player.PlayerData.MaxAmountToPile)
        {
            //Say it cant get anothe enemy!
            return;
        }

        Transform enemy = Instantiate(_enemyPilePrefab, _pileParentTransform.position, Quaternion.identity).transform;
        _pile.Add(enemy);
    }

    public void RemoveFromPile()
    {
        if (_pile.Count == 0)
        {
            //Say pile is empty!
            return;
        }

        //Remove (like a stack) the last item first!
        _pile.RemoveAt(_pile.Count-1);
    }

    void FixedUpdate()
    {
        if (_pile.Count == 0)
            return;

        _pile[0].transform.position = _pileParentTransform.transform.position;
        _pile[0].transform.rotation = _pileParentTransform.transform.rotation;

        if (_pile.Count < 2) //it needs at least 2 objects in the pile to wobble!
            return;

        WindWobble();
        FollowPlayer();
    }

    private void WindWobble()
    {
        float pingPong = Mathf.PingPong(Time.time * 0.3f, 0.25f);

        _offset = new Vector3(pingPong, 1f, pingPong);
    }

    private void FollowPlayer()
    {
        for (int i = 1; i < _pile.Count; i++)
        {
            Vector3 pos = _pile[i - 1].position +
                (_pile[i - 1].up * _offset.y)
                + (_pile[i - 1].forward * _offset.z)
                + (_pile[i - 1].right * _offset.x);

            _pile[i].position = Vector3.Lerp(_pile[i].position, pos, _speedRate);
            _pile[i].rotation = Quaternion.Lerp(_pile[i].rotation, _pile[i - 1].rotation, _speedRate);
        }
    }
}