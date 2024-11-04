using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerLevelUp : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private Experience _experience;

    [SerializeField]
    private SkinnedMeshRenderer _skinnedMeshRenderer;

    private void Start()
    {
        _player = GetComponent<Player>();

        _experience.OnLevelUp += LevelUp;
    }

    private void LevelUp()
    {
        //Aplly buffs!
        _player.PlayerData.Speed += 25f;
        _player.PlayerData.Strength += 50f;
        _player.PlayerData.Punch_Range += 0.1f;
        _player.PlayerData.MaxAmountToPile += 1;

        //Change player color and scale!
        _skinnedMeshRenderer.material.color = Random.ColorHSV();
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    private void OnDestroy()
    {
        _experience.OnLevelUp -= LevelUp;
    }
}