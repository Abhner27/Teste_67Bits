using UnityEngine;

[RequireComponent(typeof(CollectEnemies))]
public class CollectEnemiesEffects : CooldownHandlerEffectBase<CollectEnemies>
{
    [SerializeField]
    private GameObject _fillerSprite;

    private void Start()
    {
        _maximumScale = _fillerSprite.transform.localScale.x; //It could also be = transform.localScale.y

        _targetComponent.CooldownHandler.OnCooldownStart += StartCooldownEffect;
    }

    protected override void OnEffectStart()
    {
        //Get the initial scale
        _initialScale = _fillerSprite.transform.localScale;

        //Update it
        _fillerSprite.transform.localScale = new Vector3(0, 0, 1);

        //Activate the gameObject
        _fillerSprite.SetActive(true);
    }

    protected override void ApplyAdditionalEffect(float scaledValue)
    {
        //Update the scale on the x and y axis
        _fillerSprite.transform.localScale = new Vector3(scaledValue, scaledValue, 1);
    }

    protected override void OnEffectEnd()
    {
        _fillerSprite.transform.localScale = _initialScale;
        _fillerSprite.SetActive(false);
    }

    private void OnDestroy()
    {
        _targetComponent.CooldownHandler.OnCooldownStart -= StartCooldownEffect;
    }
}