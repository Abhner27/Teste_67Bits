using UnityEngine;

public class Shop : MonoBehaviour
{
    private const float BUY_EXPERIENCE_AMOUNT = 20f;
    private const int PRICE_FOR_EXPERIENCE = 100;

    [SerializeField]
    private Money _money;
    [SerializeField]
    private Experience _experience;

    //Sell enemies gain money!
    public void Sell(int value)
    {
        _money.Add(value);
    }

    //Buy experience points, gain buffs at level up!
    public void Buy()
    {
        if (_money.Reduce(PRICE_FOR_EXPERIENCE))
            _experience.Add(BUY_EXPERIENCE_AMOUNT);
    }
}