using UnityEngine;

public class Money : MonoBehaviour
{
    private int _amount = 0;
    public int Amount { get => _amount; private set { } }

    public delegate void ValueChanged(int currentAmount);
    public event ValueChanged OnValueChanged;

    public void Add(int value)
    {
        _amount += value;
        OnValueChanged?.Invoke(_amount);
    }

    public bool Reduce(int value)
    {
        if (value > _amount)
            return false;

        _amount -= value;
        OnValueChanged?.Invoke(_amount);
        return true;
    }
}
