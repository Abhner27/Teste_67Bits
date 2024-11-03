using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Experience))]
public class ExperienceUI : MonoBehaviour
{
    private Experience _experience;

    [SerializeField]
    private Image _fillImage;
    [SerializeField]
    private TextMeshProUGUI _levelText;

    private void Start()
    {
        _experience = GetComponent<Experience>();

        UpdateFillImage(0f, 1f);
        UpdateLevelText();

        _experience.OnValueChanged += UpdateFillImage;
        _experience.OnLevelUp += UpdateLevelText;
    }

    private void UpdateFillImage(float currentAmount, float max)
    {
        _fillImage.fillAmount = currentAmount / max;
    }

    private void UpdateLevelText()
    {
        _levelText.text = _experience.Level.ToString();
    }

    private void OnDestroy()
    {
        _experience.OnValueChanged -= UpdateFillImage;
        _experience.OnLevelUp -= UpdateLevelText;
    }
}
