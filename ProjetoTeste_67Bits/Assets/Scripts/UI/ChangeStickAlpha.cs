using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeStickAlpha : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Image _stickImage;

    private void Start()
    {
        if (_stickImage == null)
            _stickImage = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Changes the alpha value to 0.05f

        Color _currentColor = _stickImage.color;
        _currentColor.a = 0.05f;
        _stickImage.color = _currentColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Make it back to 0f

        Color _currentColor = _stickImage.color;
        _currentColor.a = 0f;
        _stickImage.color = _currentColor;
    }
}
