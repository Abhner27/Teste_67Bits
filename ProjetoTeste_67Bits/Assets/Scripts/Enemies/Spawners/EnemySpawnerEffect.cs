using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemySpawnerEffect : CooldownHandlerEffectBase<SpriteRenderer>
{
    //At cooldown, it changes the ring sprite color!
    //And starts an animation going from (0, 0, 1) to (0.5, 0.5, 1)

    [SerializeField]
    private EnemySpawner _enemySpawner;

    private void Start()
    {
        _maximumScale = transform.localScale.x; //It could also be = transform.localScale.y

        _enemySpawner.CooldownHandler.OnCooldownStart += StartCooldownEffect;
    }

    protected override void OnEffectStart()
    {
        //Gets the initial scale
        _initialScale = transform.localScale;

        //Update it
        transform.localScale = new Vector3(0, 0, 1);

        //Change the color
        _targetComponent.color = Color.black;
    }

    protected override void ApplyAdditionalEffect(float scaledValue)
    {
        //Update the scale on the x and y axis
        transform.localScale = new Vector3(scaledValue, scaledValue, 1);
    }

    protected override void OnEffectEnd()
    {
        _targetComponent.color = Color.white;
        transform.localScale = _initialScale;
    }

    private void OnDestroy()
    {
        _enemySpawner.CooldownHandler.OnCooldownStart -= StartCooldownEffect;
    }
}