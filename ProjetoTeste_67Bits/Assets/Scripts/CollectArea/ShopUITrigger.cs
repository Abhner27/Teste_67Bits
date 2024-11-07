using UnityEngine;

[RequireComponent(typeof(CollectArea))]
public class ShopUITrigger : MonoBehaviour
{
    private CollectArea _collectArea;

    private Vector3 _initialPosition;
    private RectTransform _shopRectTransform;

    [SerializeField]
    private GameObject _shopUI;

    private void Start()
    {
        _collectArea = GetComponent<CollectArea>();

        _shopRectTransform = _shopUI.GetComponent<RectTransform>();
        _initialPosition = _shopRectTransform.position;

        _collectArea.OnEnter += OpenShop;
        _collectArea.OnExit += CloseShop;
    }

    private void OpenShop()
    {
        _shopRectTransform.position = _initialPosition;
    }

    private void CloseShop()
    {
        _shopRectTransform.position = new Vector3(9999f, -9999f, 0f); //Set it out of the window canvas!
    }

    private void OnDestroy()
    {
        _collectArea.OnEnter -= OpenShop;
        _collectArea.OnExit -= CloseShop;
    }
}