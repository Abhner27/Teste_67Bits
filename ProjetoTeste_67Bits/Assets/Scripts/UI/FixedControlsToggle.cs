using UnityEngine;
public class FixedControlsToggle : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    [SerializeField]
    private ParticleSystem _ps;

    [SerializeField]
    private GameObject _punchUI, _dynamicJoystick, _fixedJoystick;

    public async void UpdateToggle(bool isOn)
    {
        if (isOn)
        {
            Destroy(_player.GetComponent<PlayerPunchEffects>());
            Destroy(_player.GetComponent<PlayerPunchBySelection>());

            await CustomTimeManager.WaitForGameTime(0.1f);

            _player.gameObject.AddComponent<PlayerPunchByPhysics>();
            _player.gameObject.AddComponent<PlayerPunchEffects>().UpdateParticleSystem(_ps);

            _punchUI.SetActive(true);
            _fixedJoystick.SetActive(true);
            _dynamicJoystick.SetActive(false);
        }
        else
        {
            Destroy(_player.GetComponent<PlayerPunchEffects>());
            Destroy(_player.GetComponent<PlayerPunchByPhysics>());

            await CustomTimeManager.WaitForGameTime(0.1f);

            _player.gameObject.AddComponent<PlayerPunchBySelection>();
            _player.gameObject.AddComponent<PlayerPunchEffects>().UpdateParticleSystem(_ps);

            _punchUI.SetActive(false);
            _fixedJoystick.SetActive(false);
            _dynamicJoystick.SetActive(true);
        }

    }
}
