using UnityEngine;

[RequireComponent(typeof(CollectArea))]
public class ShopUITrigger : MonoBehaviour
{
    private CollectArea _collectArea;

    [SerializeField]
    private GameObject _shopUI;

    private void Start()
    {
        _collectArea = GetComponent<CollectArea>();

        _collectArea.OnEnter += OpenShop;
        _collectArea.OnExit += CloseShop;
    }

    private void OpenShop()
    {
        _shopUI.SetActive(true);
    }

    private void CloseShop()
    {
        _shopUI.SetActive(false);
    }

    private void OnDestroy()
    {
        _collectArea.OnEnter -= OpenShop;
        _collectArea.OnExit -= CloseShop;
    }
}