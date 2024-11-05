using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraLevelUpAdjust : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;

    [SerializeField]
    private Experience _experience;

    private void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (_experience == null)
            _experience = FindFirstObjectByType<Experience>();

        _experience.OnLevelUp += AdjustVerticalFOV;
    }

    private void AdjustVerticalFOV()
    {
        _virtualCamera.m_Lens.FieldOfView += 1f;
    }

    private void OnDestroy()
    {
        _experience.OnLevelUp -= AdjustVerticalFOV;
    }
}
