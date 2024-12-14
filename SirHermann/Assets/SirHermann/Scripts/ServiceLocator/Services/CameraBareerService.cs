using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class CameraBareerService : MonoBehaviour, IService
{
    [SerializeField] private PolygonCollider2D _polygonCollider;

    private void OnValidate()
    {
        _polygonCollider = GetComponent<PolygonCollider2D>();
    }

    public PolygonCollider2D GetCameraBareer()
    {
        return _polygonCollider;
    }
}
