using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
[RequireComponent(typeof(CinemachineConfiner2D))]
public class SetCamerasSettings : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cam;
    [SerializeField] private CinemachineConfiner2D _camConfiner;

    private void OnValidate()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
        _camConfiner = GetComponent<CinemachineConfiner2D>();
    }

    private void Start()
    {
        _cam.Follow = ServiceLocator.Current.Get<PlayerMovement>().transform;
        _camConfiner.m_BoundingShape2D = ServiceLocator.Current.Get<CameraBareerService>().GetCameraBareer();
    }
}
