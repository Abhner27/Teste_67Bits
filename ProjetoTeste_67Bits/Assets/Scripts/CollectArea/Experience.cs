using UnityEngine;

public class Experience : MonoBehaviour
{
    private const float LEVEL_UP_MULTIPLIER = 1.2f;

    private float _amount = 0f;
    public float Amount { get => _amount; private set { } }

    private int _level = 0;
    public int Level { get => _level; private set { } }

    private float _levelThreshold = 50;

    public delegate void ValueChanged(float currentAmount, float max);
    public event ValueChanged OnValueChanged;

    public delegate void LevelUp();
    public event LevelUp OnLevelUp;

    public void Add(float value)
    {
        _amount += value;

        if (_amount >= _levelThreshold)  //level UP!
        {
            _amount -= _levelThreshold;

            _level++;

            _levelThreshold *= LEVEL_UP_MULTIPLIER;

            OnLevelUp?.Invoke();
        }

        OnValueChanged?.Invoke(_amount, _levelThreshold);
    }
}
