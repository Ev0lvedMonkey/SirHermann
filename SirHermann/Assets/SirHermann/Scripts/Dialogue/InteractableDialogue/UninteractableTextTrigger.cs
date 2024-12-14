using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class UninteractableTextTrigger : MonoBehaviour
{
    [SerializeField] private UninteractableText _uninteractableText;

    private void OnValidate()
    {
        if(_uninteractableText == null)
            _uninteractableText = transform.parent.GetComponent<UninteractableText>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out PlayerMovement component))
            return;
        _uninteractableText.EnableTextArea();
    }
}
