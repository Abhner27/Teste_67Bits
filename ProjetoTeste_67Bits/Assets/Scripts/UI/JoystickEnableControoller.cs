using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class JoystickEnableControoller : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] 
    private RectTransform _imageRectTransform;

    public void OnPointerDown(PointerEventData eventData)
    {
        // Converte a posição do clique para coordenadas locais do Canvas
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _imageRectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        // Centraliza a imagem na posição do clique
        _imageRectTransform.anchoredPosition = localPoint;
    }
}
