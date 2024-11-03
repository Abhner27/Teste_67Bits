using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerBuyXP : MonoBehaviour
{
    private const float BUY_EXPERIENCE_AMOUNT = 20f;
    private const int PRICE_FOR_EXPERIENCE = 100;

    private Player _player;

    [SerializeField]
    private Money _money;
    [SerializeField]
    private Experience _experience;

    private void Start()
    {
        _player = GetComponent<Player>();

        _player.PlayerActionReader.OnPlayerBuy += Buy;
    }


    //Buy experience points, gain buffs at level up!
    private void Buy()
    {
        if (_money.Reduce(PRICE_FOR_EXPERIENCE))
            _experience.Add(BUY_EXPERIENCE_AMOUNT);
    }

    private void OnDestroy()
    {
        _player.PlayerActionReader.OnPlayerBuy -= Buy;
    }
}