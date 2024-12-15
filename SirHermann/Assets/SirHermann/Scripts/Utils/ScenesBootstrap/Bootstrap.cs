using UnityEngine;

public abstract class Bootstrap : MonoBehaviour
{
    private const string ResourcesPath = "Prefabs/";

    [SerializeField] private PlayerMovement _player;
    [SerializeField] private CameraBareerService _cameraBareerService;

    protected virtual void Awake()
    {
        SetComponents();
        RegisterServices();
    }

    protected virtual void SetComponents()
    {
        GameObject playerObject = Instantiate(Resources.Load<GameObject>(ResourcesPath + "Character"));
        _cameraBareerService = FindFirstObjectByType<CameraBareerService>();
        _player = playerObject.GetComponent<PlayerMovement>();
    }

    protected virtual void RegisterServices()
    {
        ServiceLocator.Inizialize();
        ServiceLocator.Current.Register(_player);
        ServiceLocator.Current.Register(_cameraBareerService);
    }

}
