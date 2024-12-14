using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private const string ResourcesPath = "Prefabs/";

    [SerializeField] private PlayerMovement _player;
    [SerializeField] private CameraBareerService _cameraBareerService;

    private void Awake()
    {
        SetComponents();
        RegisterServices();
    }

    private void SetComponents()
    {
        GameObject playerObject = Instantiate(Resources.Load<GameObject>(ResourcesPath + "Character"));
        _cameraBareerService = FindFirstObjectByType<CameraBareerService>();
        _player = playerObject.GetComponent<PlayerMovement>();
    }

    private void RegisterServices()
    {
        ServiceLocator.Inizialize();
        ServiceLocator.Current.Register(_player);
        ServiceLocator.Current.Register(_cameraBareerService);
    }

}
