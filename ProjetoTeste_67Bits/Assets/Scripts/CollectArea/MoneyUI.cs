using UnityEngine;
using TMPro;

[RequireComponent(typeof(Money))]
public class MoneyUI : MonoBehaviour
{
    private Money _money;

    [SerializeField]
    private TextMeshProUGUI _moneyText;

    private void Start()
    {
        _money = GetComponent<Money>();

        UpdateUI(0);

        _money.OnValueChanged += UpdateUI;
    }

    private void UpdateUI(int currentAmount)
    {
        _moneyText.text = currentAmount.ToString();
    }

    private void OnDestroy()
    {
        _money.OnValueChanged -= UpdateUI;
    }
}
