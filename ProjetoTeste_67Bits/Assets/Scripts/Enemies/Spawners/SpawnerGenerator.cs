using UnityEngine;

public class SpawnerGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnerPrefab; //The prefab to instantiate

    [SerializeField]
    [Range(3f, 10f)]
    private float _distanceValue = 4f; //Distance in x and z to spread the spawners

    [SerializeField]
    private Vector3 _initialPosition; //the point from where the spawner will be created

    [SerializeField]
    private int _rows = 4, _columns = 4; //For creating a rectangle of spawners

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                Vector3 position = _initialPosition +
                    new Vector3(_distanceValue * j, _initialPosition.y, _distanceValue * i);

                GameObject nweSpawner = Instantiate(_spawnerPrefab, position, Quaternion.identity);
                nweSpawner.transform.parent = transform;
            }
        }
    }
}
