using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerPile : MonoBehaviour
{
    //Script made for creating and managing the player pile of dead enemies!

    private Player _player;

    //Pile
    private List<Transform> _pile = new List<Transform>();
    public List<Transform> Pile { get => _pile; private set { } }

    //A pile will be "active" if it has 2 or more itens in it!
    //An active pile will start the calculations for following the player!
    //It also can have other features, like "wobble"! -> Another class handles that (PileWindWobbleAnimation)
    private bool _isPileActive = false;
    public bool IsPileActive { get => _isPileActive; private set { } }


    //-----------------------------------------------------------------------------------------
    //                              Serialized references
    //-----------------------------------------------------------------------------------------

    [Header("For creating the pile")]
    [SerializeField]
    private GameObject _enemyPilePrefab;
    [SerializeField]
    private Transform _pileStartTransform; //Starter point from where the pile will be created!
    [SerializeField]
    private Vector3 _distanceBetweenItensInPile = Vector3.up;
    private Vector3 _offset = Vector3.zero;

    [Header("For following the player")]
    [SerializeField]
    private float _followPlayerSpeed = 0.5f;

    //-----------------------------------------------------------------------------------------
    //                                      Events
    //-----------------------------------------------------------------------------------------

    public delegate void PileChanges();
    public event PileChanges OnFull;
    public event PileChanges OnClear;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    public bool AddToPile()
    {
        if (_pile.Count == _player.PlayerData.MaxAmountToPile) //Pile is at the maximum capacity!
        {
            OnFull?.Invoke();
            return false;
        }

        //Calculates its initial position in the pile!
        Vector3 pos = _pileStartTransform.position + (_pile.Count * _distanceBetweenItensInPile);

        //Creates the instance of the enemyPilePrefab!
        Transform enemy = Instantiate(_enemyPilePrefab, pos, Quaternion.identity).transform;

        //Add it to the pile!
        _pile.Add(enemy);

        return true;
    }

    public void RemoveFromPile()
    {
        if (_pile.Count == 0) //Pile is empty!
            return;

        //Remove (like a stack) the last item first!
        _pile.RemoveAt(_pile.Count-1);
    }

    public void CleanPile()
    {
        if (_pile.Count == 0) //Pile is already empty!
            return;

        //Destroy the gameObjects of the enemies in the current pile!
        foreach(Transform enemyInPile in _pile)
        {
            Destroy(enemyInPile.gameObject);
        }

        //Clear the pile list
        _pile.Clear();

        //Call event
        OnClear?.Invoke();
    }

    void FixedUpdate()
    {
        if (_pile.Count == 0) //if Pile is empty, return
            return;

        //Grab the first item and make it follow the player!
        _pile[0].transform.position = _pileStartTransform.transform.position;
        _pile[0].transform.rotation = _pileStartTransform.transform.rotation;

        // it needs at least 2 objects in the pile to make it active!
        if (_pile.Count < 2)
        {
            _isPileActive = false;
            return;
        }

        _isPileActive = true;

        //Calculates the positions of the itens in the pile!
        FollowPlayer();
    }

    //To change the offset value!
    public void ApplyOffset(Vector3 offset) => _offset = offset;

    private void FollowPlayer()
    {
        //For each item in pile, calculates its postion and rotation
        //and apply an lerp based on the previous item

        for (int i = 1; i < _pile.Count; i++)
        {
            //Calculate a delay to follow based on the height of the item
            float heightRate = 1f + (i / 10f);

            //Calculate the position based on the _distanceBetweenItensInPile in every axis
            Vector3 pos = _pile[i - 1].position
                + (_pile[i - 1].right * _distanceBetweenItensInPile.x)
                + (_pile[i - 1].up * _distanceBetweenItensInPile.y)
                + (_pile[i - 1].forward * _distanceBetweenItensInPile.z);

            //Add an offset (if no effects are active, this is Vector3.zero)
            pos += _offset;

            //Update postion!
            _pile[i].position = Vector3.Lerp(_pile[i].position, pos, _followPlayerSpeed / heightRate);

            //For the rotation
            //Get the vector direction of the item and the previous item
            Vector3 direction = _pile[i - 1].position - _pile[i].position;

            //Make it look to that direction!
            Quaternion rotation = Quaternion.LookRotation(direction);

            //Apply an 90 degree angle offset in the X axis and also make the Y axis equal to the previous item
            rotation = Quaternion.Euler(new Vector3(rotation.eulerAngles.x - 90f,
                _pile[i - 1].rotation.eulerAngles.y,
                rotation.eulerAngles.z));

            //Update rotation!
            _pile[i].rotation = Quaternion.Lerp(_pile[i].rotation, rotation, _followPlayerSpeed / heightRate);
        }
    }
}