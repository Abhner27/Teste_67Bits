using UnityEngine;

public class PileEnemy : MonoBehaviour
{
    private const int MONEY_VALUE = 200;

    private static Money _money;
    private void Start()
    {
        if (_money == null)
            _money = FindFirstObjectByType<Money>();
    }

    private void OnDestroy()
    {
        _money.Add(MONEY_VALUE);
    }
}
